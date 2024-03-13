using eSusInsurers.Models;
using eSusInsurers.Models.Users.ChangePassword;
using eSusInsurers.Models.Users.Login;
using eSusInsurers.Models.Users.UpdatePassword;
using eSusInsurers.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    /// <summary>
    /// Controller for managing users.
    /// </summary>
    [Route("insurance/users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// User Registration
        /// </summary>
        /// <remarks>
        /// User Registration
        /// </remarks>
        /// <param name="request">Information of the user to register</param>
        /// <response code="201">Indicates the user is successfully created.</response>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            try
            {
                var response = await _userService.Register(request, new CancellationToken());

                return new ObjectResult(response) { StatusCode = StatusCodes.Status201Created };
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// User name existence
        /// </summary>
        /// <remarks>
        /// User name existence
        /// </remarks>
        /// <param name="userName">To check whether user name exists or not</param>
        /// <response code="200">Indicates the user existance.</response>
        [HttpGet("{userName}/check-username")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CheckUsername(string userName)
        {
            try
            {
                var response = await _userService.CheckUsername(userName, new CancellationToken());
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Send Otp to user
        /// </summary>
        /// <remarks>
        /// Send Otp to user
        /// </remarks>
        /// <param name="userName">Send otp to the userName</param>
        /// <response code="200">Indicates the otp has been successsful.</response>
        [HttpPost("{userName}/send-otp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SendOtp(string userName)
        {
            try
            {
                var response = await _userService.SendOtp(userName, new CancellationToken());
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Confirm Otp of user
        /// </summary>
        /// <remarks>
        /// COnfirm Otp od user
        /// </remarks>
        /// <param name="userName">userName of the user</param>
        /// <param name="otp">Otp sent to the userName</param>
        /// <response code="200">Indicates the otp has been confirmed.</response>
        [HttpPost("{userName}/confirm-otp/{otp}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmOtp(string userName, string otp)
        {
            try
            {
                var response = await _userService.ConfirmOtp(userName, otp, new CancellationToken());
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Update user password after confirmation of otp
        /// </summary>
        /// <remarks>
        /// Update user password after confirmation of otp
        /// </remarks>
        /// <param name="userName">userName of the user</param>
        /// <param name="updatePasswordRequest">request for the update password</param>
        /// <response code="200">Indicates the otp has been updated successfully.</response>
        [HttpPost("{userName}/update-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePassword(string userName, [FromBody] UpdatePasswordRequest updatePasswordRequest)
        {
            try
            {
                var response = await _userService.UpdatePassword(userName, updatePasswordRequest, new CancellationToken());
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// User Login
        /// </summary>
        /// <remarks>
        /// User Login
        /// </remarks>
        /// <param name="request">Information of the user to login</param>
        /// <response code="200">Indicates the user successful login.</response>
        [HttpPost("login-with-password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _userService.Login(request, new CancellationToken());
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        /// <summary>
        /// Change Password
        /// </summary>
        /// <remarks>
        /// Change Password
        /// </remarks>
        /// <param name="request">Information of the user to change password</param>
        /// <response code="200">Indicates the user password is successfully updated.</response>
        [HttpPost("change_password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                var response = await _userService.ChangePassword(request, new CancellationToken());
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }
    }
}
