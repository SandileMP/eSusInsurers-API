using eSusInsurers.Models;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for managing insurance providers.
    /// </summary>
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("insurance/insurers")]
    public class InsuranceProviderController : ControllerBase
    {
        private readonly IInsuranceProviderService _insuranceProviderService;

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
            var result = await _insuranceProviderService.CreateInuranceProvider(request, new CancellationToken());

            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }

    }
}
