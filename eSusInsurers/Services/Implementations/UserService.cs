using AutoMapper;
using EmailService.Interfaces;
using eSusInsurers.Common.Exceptions;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Helpers;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Models;
using eSusInsurers.Models.Common;
using eSusInsurers.Models.Users.ChangePassword;
using eSusInsurers.Models.Users.Login;
using eSusInsurers.Models.Users.UpdatePassword;
using eSusInsurers.Services.Interfaces;
using System.Security.Claims;
using Claim = System.Security.Claims.Claim;

namespace eSusInsurers.Services.Implementations
{
    public class UserService : IUserService
    {
        #region Fields
        private IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private IDateTime _dateTime;
        private IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly string _pepper;
        private readonly int _iteration = 3;
        private readonly ITokenService _tokenService;
        #endregion

        #region Constructor
        public UserService(IUnitOfWork unitOfWork
                         , IConfiguration configuration
                         , IMapper mapper
                         , ITokenService tokenService
                         , IDateTime dateTime
                         , IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _pepper = _configuration.GetValue<string>("PasswordHashPepper") ?? throw new Exception("Hash configuration is missing.");
            _mapper = mapper;
            _tokenService = tokenService;
            _dateTime = dateTime;
            _emailService = emailService;
        }
        #endregion

        public async Task<bool> Register(UserRegisterRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var user = await _unitOfWork.UserRepository.GetByUserNameAsync(request.UserName, cancellationToken);

            if (user != null)
                throw new Exception($"Username ({request.UserName}) already exists.");

            var userType = await _unitOfWork.UserTypeRepository.GetByIdAsync(request.UserTypeId, null, false, cancellationToken);

            if (userType == null)
                throw new Exception($"User Type Id ({request.UserTypeId}) doesn't exist.");


            user = _mapper.Map<User>(request);

            user.PasswordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, _pepper, _iteration);

            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await _unitOfWork.UserRepository.AddAsync(user, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            return true;
        }

        public async Task<string> CheckUsername(string userName, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(userName, nameof(userName));

            var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName, cancellationToken);

            if (user == null)
                throw new Exception($"Username ({userName}) doesn't exist.");

            return user.PasswordSalt;
        }

        public async Task<bool> SendOtp(string userName, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(userName, nameof(userName));

            var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName, cancellationToken);

            if (user == null)
                throw new Exception($"Username ({userName}) doesn't exist.");

            user.Otp = RandomOTP.CreateRandomOTP();

            user.OtpExipiryTime = _dateTime.Now.AddMinutes(5);

            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            var parameters = new NotificationContentParameters()
            {
                Index0 = user.Otp[0].ToString(),
                Index1 = user.Otp[1].ToString(),
                Index2 = user.Otp[2].ToString(),
                Index3 = user.Otp[3].ToString(),
            };

            _ = Task.Run(async () =>
            {
                await _emailService.SendEmailAsync(parameters, AppEvents.SendOtp, new string[] { userName }, null, cancellationToken);
            });

            return true;
        }

        public async Task<string> ConfirmOtp(string userName, string otp, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(userName, nameof(userName));

            ArgumentNullException.ThrowIfNull(otp, nameof(otp));

            var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName, cancellationToken);

            if (user == null)
                throw new Exception($"Username ({userName}) doesn't exist.");

            if (user.OtpExipiryTime < _dateTime.Now)
                throw new Exception($"Otp has been expired.");

            if (user.Otp != otp)
                throw new Exception($"Invalid Otp.");

            user.Otp = null;

            user.OtpExipiryTime = null;

            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            return user.PasswordSalt;
        }

        public async Task<bool> UpdatePassword(string userName, UpdatePasswordRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(userName, nameof(userName));

            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var user = await _unitOfWork.UserRepository.GetByUserNameAsync(userName, cancellationToken);

            if (user == null)
                throw new Exception($"Username ({userName}) doesn't exist.");

            user.PasswordHash = PasswordHasher.ComputeHash(request.NewPassword, user.PasswordSalt, _pepper, _iteration);

            user.ModifiedBy = user.UserName;

            user.ModifiedDate = _dateTime.Now;

            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            return true;
        }

        public async Task<object> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var user = await _unitOfWork.UserRepository.GetByUserNameAsync(request.Username, cancellationToken);

            if (user == null)
                throw new UnauthorizedException("Username or password did not match.");

            var passwordHash = PasswordHasher.ComputeHash(request.Password, user.PasswordSalt, _pepper, _iteration);

            if (user.PasswordHash != passwordHash)
                throw new UnauthorizedException("Username or password did not match.");

            var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, user.UserType.UserType1),
        };
            var accessToken = _tokenService.GenerateAccessToken(claims);

            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;

            _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

            user.RefreshTokenExpiryTime = _dateTime.Now.AddDays(refreshTokenValidityInDays);

            user.LastLoggedInDate = _dateTime.Now;

            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            return new AuthenticatedResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                UserId = user.Id,
            };
        }

        public async Task<object> ChangePassword(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request, nameof(request));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(request.UserId, null, false, cancellationToken);

            if (user == null)
                return new UnauthorizedException("User Id doesn't exist.");

            var passwordHash = PasswordHasher.ComputeHash(request.OldPassword, user.PasswordSalt, _pepper, _iteration);

            if (user.PasswordHash != passwordHash)
                return new UnauthorizedException("Incorrect old password.");

            user.PasswordHash = PasswordHasher.ComputeHash(request.NewPassword, user.PasswordSalt, _pepper, _iteration);

            user.ModifiedBy = user.UserName;

            user.ModifiedDate = _dateTime.Now;

            var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                await _unitOfWork.UserRepository.UpdateAsync(user, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);

                throw;
            }

            return true;
        }

    }
}
