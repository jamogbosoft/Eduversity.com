using Eduversity.com.Shared.Dtos.EmailDto;

namespace Eduversity.com.Client.Services.EmailService
{
    public interface IEmailService
    {
        Task<ServiceResponse<bool>> SendEmail(EmailResponse request);
    }
}
