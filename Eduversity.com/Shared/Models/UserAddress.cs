using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduversity.com.Shared.Models
{
    public class UserAddress
    {
        [Key]
        public long UserId { get; set; }
        [StringLength(150)]
        public string PermanentAddress { get; set; } = string.Empty;
        [StringLength(150)]
        public string ContactAddress { get; set; } = string.Empty;
        [StringLength(15)]
        public string MobileNumber { get; set; } = string.Empty;
        [StringLength(15)]
        public string TelephoneNumber { get; set; } = string.Empty;
        [StringLength(45)]
        public string EmailAddress { get; set; } = string.Empty;
        [StringLength(30)]
        public string NextOfKin { get; set; } = string.Empty;
        [StringLength(45)]
        public string NextOfKinEmailAddress { get; set; } = string.Empty;
        [StringLength(15)]
        public string NextofKinMobileNumber { get; set; } = string.Empty;
        [StringLength(15)]
        public string NextOfKinTelephoneNumber { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int LGAId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
