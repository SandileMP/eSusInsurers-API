using eSusInsurers.Models.Users.RefreshAccessToken;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// User Token Management
    /// </summary>
    [Route("insurance/token")]
    public class TokenController : BaseController
    {
        #region Fields
        private readonly ITokenService _tokenService;
        #endregion

        #region Constructor
        public TokenController(ITokenService tokenService)
        {
            this._tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
        } 
        #endregion

        /// <summary>
        /// Refresh Access Token
        /// </summary>
        /// <remarks>
        /// Refresh Access Token
        /// </remarks>
        /// <param name="tokenApiModel">Information of the user access token and refresh token</param>
        /// <response code="200">Indicates the user token refreshed successfully.</response>
        [HttpPost("refresh")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Refresh(TokenApiModel tokenApiModel)
        {
            try
            {
                var response = await _tokenService.RefreshToken(tokenApiModel, new CancellationToken());

                return new ObjectResult(response) { StatusCode = StatusCodes.Status200OK };
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    ErrorMessage = e.Message
                });
            }

        }

        /// <summary>
        /// Refresh Access Token
        /// </summary>
        /// <remarks>
        /// Refresh Access Token
        /// </remarks>
        /// <param name="tokenApiModel">Information of the user access token and refresh token</param>
        /// <response code="200">Indicates the refresh token is revoked.</response>
        [HttpPost("revoke"), Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Revoke()
        {
            try
            {
                var response = await _tokenService.RevokeToken(User.Identity.Name, new CancellationToken());

                return new ObjectResult(response) { StatusCode = StatusCodes.Status204NoContent };
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    ErrorMessage = e.Message
                });
            }
        }
    }
}
