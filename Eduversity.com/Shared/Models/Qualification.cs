using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Eduversity.com.Shared.Models
{
    public class Qualification
    {
        [Key]
        public int Id { get; set; }
        public int LecturerId { get; set; } // Foreign key  
        [Required, MaxLength(10)]
        public string Degree { get; set; } = string.Empty;
        [Required, MaxLength(50)] //Applied Biology and Biotechnology
        public string CourseOfStudy { get; set; } = string.Empty;
        [Required, MaxLength(70)] // Federal University of Science and Technology, Akwa-Ibom
        public string Institution { get; set; } = string.Empty;
        [Required, MaxLength(4)]
        public string YearGraduated { get; set; } = string.Empty;

        [JsonIgnore]
        
        [ForeignKey(nameof(LecturerId))]
        public LecturerAcademicDetail? LecturerAcademicDetail { get; set; } // Reference navigation
    }
}
