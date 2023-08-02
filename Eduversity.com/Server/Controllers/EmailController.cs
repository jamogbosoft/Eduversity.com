using Eduversity.com.Server.Services.EmailService;
using Eduversity.com.Shared.Dtos.EmailDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send-email")]
        public async Task<ActionResult<ServiceResponse<bool>>> SendEmail(EmailResponse request)
        {
            var response = await _emailService.SendEmail(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
