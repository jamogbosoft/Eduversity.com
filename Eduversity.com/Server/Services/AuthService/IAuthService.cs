namespace Eduversity.com.Server.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<long>> SignUpAsync(User user, string password);
        Task<bool> UserExists(string username);
        Task<ServiceResponse<string>> SignInAsync(string username, string password);
        Task<ServiceResponse<bool>> ChangePasswordAsync(long userId, UserChangePassword request);
        long GetUserId();
        string GetUserName();
        Task<User> GetUserByUserName(string username);
    }
}
