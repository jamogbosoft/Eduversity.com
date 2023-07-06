using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Models
{
    public  class User
    {
        [Key]
        public long Id { get; set; }
        [Required, EmailAddress]
        [StringLength(70, ErrorMessage = "UserName should not exceed 70 characters.")]
        public string UserName { get; set; } = string.Empty;
        [Required, EmailAddress]
        [StringLength(70, ErrorMessage = "Email should not exceed 70 characters.")]
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public string? VerificationToken { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }

        public virtual UserAddress? Address { get; set; } // Reference navigation
        public virtual UserRole? UserRole { get; set; } // Reference navigation
    }
}
