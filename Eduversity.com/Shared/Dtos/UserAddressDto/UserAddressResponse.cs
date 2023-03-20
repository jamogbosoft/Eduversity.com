using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.UserAddressDto
{
    public class UserAddressResponse
    {
        public long UserId { get; set; }
        [EmailAddress]
        [StringLength(45, ErrorMessage = "Email Address should not exceed 45 characters.")]
        public string EmailAddress { get; set; } = string.Empty;
        [StringLength(15, ErrorMessage = "Mobile Number should not exceed 15 characters.")]
        public string MobileNumber { get; set; } = string.Empty;
        [StringLength(15, ErrorMessage = "Telephone Number should not exceed 15 characters.")]
        public string TelephoneNumber { get; set; } = string.Empty;
        [StringLength(30, ErrorMessage = "Next of Kin should not exceed 30 characters.")]
        public string NextOfKin { get; set; } = string.Empty;
       //[EmailAddress]
        [StringLength(45, ErrorMessage = "Next of Kin Email Address should not exceed 45 characters.")]
        public string NextOfKinEmailAddress { get; set; } = string.Empty;
        [StringLength(15, ErrorMessage = "Next of Kin Mobile Number should not exceed 15 characters.")]
        public string NextofKinMobileNumber { get; set; } = string.Empty;

        [StringLength(15, ErrorMessage = "Next of Kin Telephone Number should not exceed 15 characters.")]
        public string NextOfKinTelephoneNumber { get; set; } = string.Empty;
        [StringLength(150, ErrorMessage = "Permanent Address should not exceed 150 characters.")]
        public string PermanentAddress { get; set; } = string.Empty;
        [StringLength(150, ErrorMessage = "Contact Address should not exceed 150 characters.")]
        public string ContactAddress { get; set; } = string.Empty;
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int LGAId { get; set; }
        public string CountryName { get; set; } = string.Empty;
        public string StateName { get; set; } = string.Empty;
        public string LGAName { get; set; } = string.Empty;
    }
}
