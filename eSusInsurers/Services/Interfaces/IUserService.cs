using eSusInsurers.Models;
using eSusInsurers.Models.Users.ChangePassword;
using eSusInsurers.Models.Users.Login;
using eSusInsurers.Models.Users.UpdatePassword;

namespace eSusInsurers.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(UserRegisterRequest request, CancellationToken cancellationToken);

        Task<bool> SendOtp(string userName, CancellationToken cancellationToken);

        Task<object> Login(LoginRequest resource, CancellationToken cancellationToken);

        Task<object> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken);

        Task<string> CheckUsername(string userName, CancellationToken cancellationToken);

        Task<string> ConfirmOtp(string userName, string otp, CancellationToken cancellationToken);

        Task<bool> UpdatePassword(string userName, UpdatePasswordRequest request, CancellationToken cancellationToken);
    }
}
