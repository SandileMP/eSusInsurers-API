using eSusInsurers.Models.InsuranceProviders.CreateInsuranceProvider;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for managing insurance providers.
    /// </summary>
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("insurance/insurers")]
    public class InsuranceProviderController : BaseController
    {
        private readonly IInsuranceProviderService _insuranceProviderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="InsuranceProviderController"/> class.
        /// </summary>
        /// <param name="insuranceProviderService">The instance of insurance provider service.</param>
        public InsuranceProviderController(IInsuranceProviderService insuranceProviderService)
        {
            _insuranceProviderService = insuranceProviderService;
        }

        /// <summary>
        /// Create insurance provider
        /// </summary>
        /// <remarks>
        /// Creates a new insurance provider.
        /// </remarks>
        /// <param name="request">Information of the insurance provider to be created.</param>
        /// <response code="201">Indicates the insurance provider was successfully created.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateInsuranceProviderResponse))]
        public async Task<ActionResult<CreateInsuranceProviderResponse>> CreateInsuranceProvider(
            [FromBody] CreateInsuranceProviderRequest request)
        {
            var result = await _insuranceProviderService.CreateInsuranceProvider(request, new CancellationToken());

            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

    }
}
