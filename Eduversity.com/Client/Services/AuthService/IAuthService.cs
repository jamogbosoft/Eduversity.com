using Eduversity.com.Shared.Dtos.UserAccountDto;

namespace Eduversity.com.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<long>> Register(UserRegisterRequest request);
        Task<ServiceResponse<string>> Login(UserLoginRequest request);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordRequest request);
        Task<ServiceResponse<bool>> VerifyEmailAsync(UserEmailVerificationRequest request);
        Task<ServiceResponse<bool>> ForgotPasswordAsync(UserForgotPasswordRequest request);
        Task<ServiceResponse<bool>> ResetPasswordAsync(UserPasswordResetRequest request);
        Task<ServiceResponse<bool>> ResendVerificationTokenAsync(UserResendVerificationTokenRequest request);
        Task<bool> IsUserAuthenticated();
    }
}
