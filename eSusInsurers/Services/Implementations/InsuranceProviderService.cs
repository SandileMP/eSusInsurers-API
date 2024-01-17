using AutoMapper;
using AzureIntegrations.API.Interfaces;
using AzureIntegrations.API.Models;
using eSusInsurers.Common;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models;
using eSusInsurers.Services.Interfaces;

namespace eSusInsurers.Services.Implementations
{
    public class InsuranceProviderService : IInsuranceProviderService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly IInternaleSusFarmService _internaleSusFarmService;
        private readonly IAzureFileStorageConnection _azureFileStorageConnection;
        private readonly ILoggerContext<InsuranceProvider> _logger;



        public InsuranceProviderService(IUnitOfWork unitOfWork, IMapper mapper, IAzureFileStorageConnection azureFileStorageConnection, ILoggerContext<InsuranceProvider> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_internaleSusFarmService = internaleSusFarmService;
            _azureFileStorageConnection = azureFileStorageConnection;
            _logger = logger;
        }

        public async Task<CreateInsuranceProviderResponse> CreateInuranceProvider(CreateInsuranceProviderRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            //var getprovinces = await _internaleSusFarmService.GetProvinceSummary(cancellationToken);

            var insuranceProvider = _mapper.Map<InsuranceProvider>(request);
            insuranceProvider.CreatedDate = insuranceProvider.ModifiedDate = DateTime.UtcNow;

            using var transaction = _unitOfWork.BeginTransaction();

            try
            {

                await _unitOfWork.InsuranceProviderRepository.AddAsync(insuranceProvider, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                if (request.InsuranceProviderFiles?.Count() > 0)
                {
                    var insuranceProviderDocuments = await UploadInsuranceProviderFiles(request.InsuranceProviderFiles.ToList(), insuranceProvider.Id);

                    if (insuranceProviderDocuments.Count > 0)
                    {
                        await _unitOfWork.InsuranceProviderDocumentRepository.AddRangeAsync(insuranceProviderDocuments, cancellationToken);

                        await _unitOfWork.SaveChangesAsync(cancellationToken);
                    }
                }

                await transaction.CommitAsync(cancellationToken);

                return new CreateInsuranceProviderResponse()
                {
                    InsuranceProviderId = insuranceProvider.Id,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                await _logger.LogErrorAsync(insuranceProvider, ex, "An error", cancellationToken);

                await transaction.RollbackAsync(cancellationToken);

                return new CreateInsuranceProviderResponse()
                {
                    Success = false,
                    FailureReason = ex.Message
                };
            }
        }

        private async Task<List<InsuranceProviderDocument>> UploadInsuranceProviderFiles(List<InsuranceProviderFiles> InsuranceProviderFiles, int insuranceProviderId)
        {

            List<InsuranceProviderDocument> insuranceProviderDocuments = new List<InsuranceProviderDocument>();

            try
            {

                foreach (var file in InsuranceProviderFiles)
                {
                    if (file.DocumentData != null)
                    {
                        AzureDocument document = new AzureDocument();
                        document.DocumentName = $"{insuranceProviderId}_{file.DocumentName}";
                        document.DocumentData = Convert.FromBase64String(file.DocumentData);
                        document.MainDirectory = "InsuranceProviderDocuments";
                        document.ChildDirectory = insuranceProviderId.ToString();

                        var documentPath = await _azureFileStorageConnection.UploadInsuranceProviderFiles(document);

                        if (documentPath != null)
                        {
                            InsuranceProviderDocument insuranceProviderDocument = new InsuranceProviderDocument();
                            insuranceProviderDocument.InsuranceProviderId = insuranceProviderId;
                            insuranceProviderDocument.DocumentName = file.DocumentName;
                            insuranceProviderDocument.DocumentPath = documentPath;
                            insuranceProviderDocument.IsActive = true;

                            insuranceProviderDocuments.Add(insuranceProviderDocument);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return insuranceProviderDocuments;
        }
    }
}
