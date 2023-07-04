using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eduversity.com.Shared.Models
{
    public class Student
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; } // Foreign key    

        [Required, MaxLength(20)]
        public string FirstName { get; set; } = string.Empty;
        
        [MaxLength(20)]
        public string MiddleName { get; set; } = string.Empty;

        [Required, MaxLength(25)]
        public string LastName { get; set; } = string.Empty;
        
        [MaxLength(10)]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(150)]
        public string PlaceOfBirth { get; set; } = string.Empty;

        [Required, MaxLength(1)]
        public string Gender { get; set; } = string.Empty;

        [Required, MaxLength(1)]
        public string MaritalStatus { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        [JsonIgnore]

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; } // Reference navigation
        public StudentAcademicDetail AcademicDetail { get; set; } = new();
        //public UserAddress Address { get; set; } = new();
    }
}