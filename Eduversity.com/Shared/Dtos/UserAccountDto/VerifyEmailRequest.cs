using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.UserAccountDto
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required, EmailAddress]
        [StringLength(70, ErrorMessage = "Email should not exceed 70 characters.")]
        public string Email { get; set; } = string.Empty;
    }
}
