namespace Eduversity.com.Server.Services.UserAddressService
{
    public interface IUserAddressService
    {
        Task<ServiceResponse<UserAddressResponse>> GetAddress(long userId = 0L);
        Task<ServiceResponse<UserAddressResponse>> AddOrUpdateAddress(UserAddressResponse address);
    }
}
