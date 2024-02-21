using eSusInsurers.Controllers;
using eSusInsurers.Models;
using eSusInsurers.Services.Interfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tynamix.ObjectFiller;
using Xunit;

namespace eSusInsurers.Tests.Controllers
{
    public class InsuranceProviderControllerTest
    {
        #region Fields
        private readonly IInsuranceProviderService _insuranceProviderService;
        #endregion

        public InsuranceProviderControllerTest()
        {
            _insuranceProviderService = A.Fake<IInsuranceProviderService>();
        }

        private static CreateInsuranceProviderRequest CreateRandomCreateInsuranceProviderRequest()
        {
            var filler = new Filler<CreateInsuranceProviderRequest>();
            return filler.Create();
        }

        private static CreateInsuranceProviderResponse CreateRandomCreateInsuranceProviderResponse()
        {
            var filler = new Filler<CreateInsuranceProviderResponse>();
            return filler.Create();
        }

        [Fact]
        [Trait("Controller", "Post")]
        public async Task CreateInsuranceProvider_WhenPostedValidCreateInsuranceProviderRequest_ReturnsHttpStatusCode201()
        {
            // Arrange
            var expectedResult = CreateRandomCreateInsuranceProviderResponse();
            var request = CreateRandomCreateInsuranceProviderRequest();


            A.CallTo(() => _insuranceProviderService.CreateInsuranceProvider(A<CreateInsuranceProviderRequest>.Ignored, A<CancellationToken>.Ignored)).Returns(expectedResult);

            var insuranceProviderController = new InsuranceProviderController(_insuranceProviderService);

            // Act
            var actionResult = await insuranceProviderController.CreateInsuranceProvider(request);
            var objectResult = actionResult.Result as ObjectResult;

            // Assert
            actionResult.Should().NotBeNull();
            objectResult.Value.Should().BeEquivalentTo(expectedResult);
            objectResult.StatusCode.Should().Be(StatusCodes.Status201Created);
        }

        [Fact]
        [Trait("Controller", "Post")]
        public async Task CreatePartInventory_WhenPostedInvalidCreatePartInventoryInboundDto_ReturnsValidationException()
        {
            // Arrange

            var expectedResult = CreateRandomCreateInsuranceProviderResponse();
            var request = CreateRandomCreateInsuranceProviderRequest();

            A.CallTo(() => _insuranceProviderService.CreateInsuranceProvider(A<CreateInsuranceProviderRequest>._, A<CancellationToken>._)).Throws<Exception>();

            var insuranceProviderController = new InsuranceProviderController(_insuranceProviderService);

            // Act
            Func<Task> act = async () => await insuranceProviderController.CreateInsuranceProvider(request);

            // Assert
            await act.Should().ThrowAsync<Exception>();

        }
    }
}
