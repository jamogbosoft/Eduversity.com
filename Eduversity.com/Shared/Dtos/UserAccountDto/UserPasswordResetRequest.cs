using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.UserAccountDto
{
    public class UserPasswordResetRequest
    {
        [Required, EmailAddress]
        [StringLength(70, ErrorMessage = "Email should not exceed 70 characters.")]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 6, ErrorMessage = "Please enter at least 6 characters.")]
        public string NewPassword { get; set; } = string.Empty;
        [Required, Compare("NewPassword", ErrorMessage = "'Password' and 'Confirm Password' do not match.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;
        
    }
}
