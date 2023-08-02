using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.UserAccountDto
{
    public class UserEmailRequest
    {
        [Required, EmailAddress, MaxLength(70, ErrorMessage = "Email should not exceed 70 characters.")]
        public string Email { get; set; } = string.Empty;
    }
}
