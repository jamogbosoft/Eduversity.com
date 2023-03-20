using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.UserAccountDto
{
    public class UserChangePassword
    {
        [Required, StringLength(100, MinimumLength = 6)]
        [DisplayName("Old Password")]
        public string OldPassword { get; set; } = string.Empty;

        [Required, StringLength(100, MinimumLength = 6)]
        [DisplayName("New Password")]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [Compare("NewPassword", ErrorMessage = "The passwords do not match.")]
        [DisplayName("Confirm New Password")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
