using System.Security.Claims;

namespace Eduversity.com.Client.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly AuthenticationStateProvider _authStateProvider;

        public AdminService(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }
        public async Task<bool> IsAuthorized()
        {
            try
            {
                var result = await _authStateProvider.GetAuthenticationStateAsync();
                var claim = result.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
                string role = claim != null ? claim.Value : string.Empty;

                if (role.Contains("Admin"))
                {
                    return true;
                }
            }
            catch { }
            return false;
        }
    }
}
