namespace Eduversity.com.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<long>> Register(UserRegisterRequest request);
        Task<ServiceResponse<string>> Login(UserLoginRequest request);
        Task<ServiceResponse<bool>> ChangePassword(UserChangePasswordRequest request);
        Task<bool> IsUserAuthenticated();
    }
}
