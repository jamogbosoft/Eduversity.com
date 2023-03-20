using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eduversity.com.Shared.Models
{
    public class Lecturer
    {
        [Key]
        public int Id { get; set; }
        public long UserId { get; set; } // Foreign key    

        [Required, MaxLength(45)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(1)]
        public string Gender { get; set; } = string.Empty;

        [Required, MaxLength(1)]
        public string MaritalStatus { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        [JsonIgnore]

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; } // Reference navigation
        public LecturerAcademicDetail AcademicDetail { get; set; } = new();
        public List<CourseAllocation> CourseAllocations { get; set; } = new();
    }
}
