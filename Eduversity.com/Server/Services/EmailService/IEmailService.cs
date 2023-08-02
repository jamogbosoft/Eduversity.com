using Eduversity.com.Shared.Dtos.EmailDto;

namespace Eduversity.com.Server.Services.EmailService
{
    public interface IEmailService
    {
        Task<ServiceResponse<bool>> SendEmail(EmailResponse request);
    }
}
