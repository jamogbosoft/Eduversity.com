using Eduversity.com.Shared.Dtos.EmailDto;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System.Security.AccessControl;

namespace Eduversity.com.Server.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<ServiceResponse<bool>> SendEmail(EmailResponse request)
        {
            int emailPort = 0;
            string emailHost = _config.GetSection("EmailHost").Value!;
            if (int.TryParse(_config.GetSection("EmailPort").Value, out emailPort)) { }
            string emailUsername = _config.GetSection("EmailUsername").Value!;
            string emailPassword = _config.GetSection("EmailPassword").Value!;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(emailUsername));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(emailHost, emailPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(emailUsername, emailPassword);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);

            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Email sent"
            };
        }
    }
}
