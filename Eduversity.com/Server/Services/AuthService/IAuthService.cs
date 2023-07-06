using Eduversity.com.Shared.Dtos.UserAccountDto;

namespace Eduversity.com.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<long>> SignUpAsync(UserRegisterRequest request);
        Task<bool> UserExists(string username);
        Task<ServiceResponse<string>> SignInAsync(UserLoginRequest request);
        Task<ServiceResponse<bool>> ChangePasswordAsync(long userId, UserChangePasswordRequest request);
        Task<ServiceResponse<bool>> VerifyEmailAsync(VerifyEmailRequest request);
        Task<ServiceResponse<bool>> ForgotPasswordAsync(string email);
        Task<ServiceResponse<bool>> ResetPasswordAsync(ResetPasswordRequest request);
        long GetUserId();
        string GetUserName();
        Task<User> GetUserByUserName(string username);
    }
}
