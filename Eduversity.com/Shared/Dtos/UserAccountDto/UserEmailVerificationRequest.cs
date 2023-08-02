using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.UserAccountDto
{
    public class UserEmailVerificationRequest
    {   
        [Required, EmailAddress]
        [StringLength(70, ErrorMessage = "Email should not exceed 70 characters.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
