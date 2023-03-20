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
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public virtual UserAddress? Address { get; set; } // Reference navigation
        public virtual UserRole? UserRole { get; set; } // Reference navigation
    }
}
