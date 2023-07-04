using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.UserAccountDto
{
    public class UserRegisterRequest
    {
        [Required, MaxLength(20, ErrorMessage = "Role should not exceed 20 characters.")]
        public string Role { get; set; } = string.Empty;
        [Required, EmailAddress]
        [StringLength(100, ErrorMessage = "Email should not exceed 100 characters.")]
        public string UserName { get; set; } = string.Empty;
        [Required, StringLength(100, MinimumLength = 6, ErrorMessage ="Please enter at least 6 characters.")]
        public string Password { get; set; } = string.Empty;
        [Required, Compare("Password", ErrorMessage = "'Password' and 'Confirm Password' do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}



