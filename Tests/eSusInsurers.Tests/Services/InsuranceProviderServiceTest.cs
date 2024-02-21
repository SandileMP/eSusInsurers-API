using AutoMapper;
using AzureIntegrations.API.Interfaces;
using AzureIntegrations.API.Models;
using AzureIntegrations.API.Services;
using eSusFarmInternal.API.Interfaces;
using eSusFarmInternal.API.Models;
using eSusInsurers.Common.Logging;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Models;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Tynamix.ObjectFiller;
using Xunit;

namespace eSusInsurers.Tests.Services
{
    public class InsuranceProviderServiceTest
    {
        private readonly IUnitOfWork _fakeUnitOfWork;
        private readonly IMapper _fakeMapper;
        private readonly IInternaleSusFarmService _fakeInternaleSusFarmService;
        private readonly IAzureFileStorageConnection _fakeAzureFileStorageConnection;
        private readonly ILoggerContext<InsuranceProvider> _fakeLogger;
        private readonly IInsuranceProviderService _insuranceProviderService;


        public InsuranceProviderServiceTest()
        {
            _fakeUnitOfWork = A.Fake<IUnitOfWork>();
            _fakeMapper = A.Fake<IMapper>();
            _fakeInternaleSusFarmService = A.Fake<IInternaleSusFarmService>();
            _fakeAzureFileStorageConnection = A.Fake<IAzureFileStorageConnection>();
            _fakeLogger = A.Fake<ILoggerContext<InsuranceProvider>>();
            _insuranceProviderService = new InsuranceProviderService(
                _fakeUnitOfWork,
                _fakeMapper,
                _fakeAzureFileStorageConnection,
                _fakeLogger,
                _fakeInternaleSusFarmService
            );
        }

        private CreateInsuranceProviderRequest RandomCreateInsuranceProviderRequest()
        {
            var filler = new Filler<CreateInsuranceProviderRequest>();

            filler.Setup()
                .OnProperty(x => x.InsuranceProviderName).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(x => x.ContactPersonName).Use(new RealNames(NameStyle.FirstName))
                .OnProperty(x => x.ContactPersonMobileNumber).Use(2321324324)
                .OnProperty(x => x.ContactPersonAlternateContactNumber).Use(999999999)
                .OnProperty(x => x.ContactPersonEmailId).Use("test@test.com")
                .OnProperty(x => x.ContactPersonAlternateEmailId).Use("test@test.com")
                .OnProperty(x => x.Country).Use("Africa")
                .OnProperty(x => x.CountryId).Use(new IntRange(1, 100))
                .OnProperty(x => x.HeadOfficeAddress).Use("test")
                .OnProperty(x => x.EmailId1).Use("test@test.com")
                .OnProperty(x => x.EmailId2).Use("test1@test.com")
                .OnProperty(x => x.ContactNumber1).Use(324234234)
                .OnProperty(x => x.ContactNumber2).Use(321312312)
                .OnProperty(x => x.loggedInUser).Use("test")
                .OnProperty(x => x.InsuranceProviderFiles).Use(new List<InsuranceProviderFiles>(){
                    new InsuranceProviderFiles()
                {
                    DocumentData = "test",
                    DocumentName = "test"
                }
                });


            return filler.Create();
        }

        [Fact]
        public async Task CreateInsuranceProvider_WithValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var request = RandomCreateInsuranceProviderRequest();

            // Mock dependencies as needed
            A.CallTo(() => _fakeInternaleSusFarmService.GetProvinceSummary(A<CancellationToken>._)).Returns(new List<ProvincesDto?>());
            A.CallTo(() => _fakeUnitOfWork.SaveChangesAsync(A<CancellationToken>._)).Returns(Task.FromResult(0));
            A.CallTo(() => _fakeAzureFileStorageConnection.UploadInsuranceProviderFiles(A<AzureDocument>.Ignored)).Returns("test");

            // Act
            var result = await _insuranceProviderService.CreateInsuranceProvider(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.FailureReason.Should().BeNull();
        }

        [Fact]
        public async Task CreateInsuranceProvider_WithValidRequestAndWithoutDocuments_ReturnsSuccessResponse()
        {
            // Arrange
            var request = RandomCreateInsuranceProviderRequest();
            request.InsuranceProviderFiles = null;

            // Mock dependencies as needed
            A.CallTo(() => _fakeInternaleSusFarmService.GetProvinceSummary(A<CancellationToken>._)).Returns(new List<ProvincesDto?>());
            A.CallTo(() => _fakeUnitOfWork.SaveChangesAsync(A<CancellationToken>._)).Returns(Task.FromResult(0));
            A.CallTo(() => _fakeAzureFileStorageConnection.UploadInsuranceProviderFiles(A<AzureDocument>.Ignored)).Returns("test");

            // Act
            var result = await _insuranceProviderService.CreateInsuranceProvider(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.FailureReason.Should().BeNull();
        }

        [Fact]
        public async Task CreateInsuranceProvider_WithInValidRequest_ReturnsFalseResponse()
        {
            // Arrange
            var request = RandomCreateInsuranceProviderRequest();

            // Mock dependencies as needed
            A.CallTo(() => _fakeInternaleSusFarmService.GetProvinceSummary(A<CancellationToken>._)).Returns(new List<ProvincesDto?>());
            A.CallTo(() => _fakeUnitOfWork.SaveChangesAsync(A<CancellationToken>._)).Throws<Exception>();
            A.CallTo(() => _fakeAzureFileStorageConnection.UploadInsuranceProviderFiles(A<AzureDocument>.Ignored)).Returns("test");

            // Act
            var result = await _insuranceProviderService.CreateInsuranceProvider(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.FailureReason.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateInsuranceProvider_WhenStorageThrowsException_ReturnsFalseReponse()
        {
            // Arrange
            var request = RandomCreateInsuranceProviderRequest();

            // Mock dependencies as needed
            A.CallTo(() => _fakeInternaleSusFarmService.GetProvinceSummary(A<CancellationToken>._)).Returns(new List<ProvincesDto?>());
            A.CallTo(() => _fakeUnitOfWork.SaveChangesAsync(A<CancellationToken>._)).Returns(Task.FromResult(0));
            A.CallTo(() => _fakeAzureFileStorageConnection.UploadInsuranceProviderFiles(A<AzureDocument>.Ignored)).Throws<Exception>();

            // Act
            var result = await _insuranceProviderService.CreateInsuranceProvider(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.FailureReason.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateInsuranceProvider_WithNullRequest_ThrowsArgumentNullException()
        {
            // Arrange

            // Act
            Func<Task> act = async () => await _insuranceProviderService.CreateInsuranceProvider(null, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'request')");
        }
    }
}
