namespace Eduversity.com.Client.Services.UserAddressService
{
    public interface IUserAddressService
    {
        Task<UserAddressResponse> GetAddress(long userId = 0L);
        Task<UserAddressResponse> AddOrUpdateAddress(UserAddressResponse address);
    }
}
