using Eduversity.com.Shared.Dtos.EmailDto;
using static System.Net.WebRequestMethods;

namespace Eduversity.com.Client.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _http;
        public EmailService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ServiceResponse<bool>> SendEmail(EmailResponse request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/send-email", request);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            return response!;
        }
    }
}
