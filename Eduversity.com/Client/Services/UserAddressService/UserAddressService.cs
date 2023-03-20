
namespace Eduversity.com.Client.Services.UserAddressService
{
    public class UserAddressService : IUserAddressService
    {
        private readonly HttpClient _http;

        public UserAddressService(HttpClient http)
        {
            _http = http;
        }

        public async Task<UserAddressResponse> AddOrUpdateAddress(UserAddressResponse address)
        {
            var response = await _http.PostAsJsonAsync("api/useraddresses", address);            
            var result = await response.Content
                .ReadFromJsonAsync<ServiceResponse<UserAddressResponse>>();
            if (result == null || result.Data == null)
            {
                return null!;
            }
            return result.Data;
        }
                
        public async Task<UserAddressResponse> GetAddress(long userId = 0L)
        {
            var response = userId == 0L ?
                await _http.GetFromJsonAsync<ServiceResponse<UserAddressResponse>>("api/useraddresses") :
                await _http.GetFromJsonAsync<ServiceResponse<UserAddressResponse>>($"api/useraddresses/{userId}");

            if (response == null || response.Data == null)
            {
                return null!;
            }
            return response.Data;

        }
    }
}
