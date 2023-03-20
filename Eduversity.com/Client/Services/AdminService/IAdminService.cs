namespace Eduversity.com.Client.Services.AdminService
{
    public interface IAdminService
    {
        Task<bool> IsAuthorized();
    }
}
