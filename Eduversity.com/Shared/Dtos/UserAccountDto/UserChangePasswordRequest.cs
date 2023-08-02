using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.UserAccountDto
{
    public class UserChangePasswordRequest
    {
        [Required, StringLength(100, MinimumLength = 6)]
        public string OldPassword { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 6)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPassword", ErrorMessage = "The passwords do not match.")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
