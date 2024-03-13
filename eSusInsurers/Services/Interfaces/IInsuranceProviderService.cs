using eSusInsurers.Models.InsuranceProviders.CreateInsuranceProvider;

namespace eSusInsurers.Services.Interfaces
{
    public interface IInsuranceProviderService
    {
        Task<CreateInsuranceProviderResponse> CreateInsuranceProvider(CreateInsuranceProviderRequest request, CancellationToken cancellationToken);
    }
}
