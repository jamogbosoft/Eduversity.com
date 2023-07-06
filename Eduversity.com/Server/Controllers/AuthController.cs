using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<long>>> SignUpAsync(UserRegisterRequest request)
        {
            var response = await _authService.SignUpAsync(request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> SignInAsync(UserLoginRequest request)
        {
            var response = await _authService.SignInAsync(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("change-password"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePasswordAsync(UserChangePasswordRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.ChangePasswordAsync(long.Parse(userId!), request);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("verify-email")]
        public async Task<ActionResult<ServiceResponse<bool>>> VerifyEmailAsync(VerifyEmailRequest request)
        {
            var response = await _authService.VerifyEmailAsync(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<ServiceResponse<bool>>> ForgotPasswordAsync(string email)
        {
            var response = await _authService.ForgotPasswordAsync(email);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ServiceResponse<bool>>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var response = await _authService.ResetPasswordAsync(request);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
