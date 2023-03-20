using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.UserAccountDto
{
    public class UserLogin
    {
        [Required, EmailAddress]
        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters.")]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
