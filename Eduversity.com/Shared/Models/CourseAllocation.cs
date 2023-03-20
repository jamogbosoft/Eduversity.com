using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduversity.com.Shared.Models
{
    public class CourseAllocation
    {
        public int LecturerId { get; set; } // Foreign key   
        public int CourseId { get; set; }   // Foreign key   
        [Required, MaxLength(9)]
        public string Session { get; set; } = string.Empty;

        [ForeignKey(nameof(LecturerId))]
        public Lecturer? Lecturer { get; set; } // Reference navigation
        [ForeignKey(nameof(CourseId))]
        public Course? Course { get; set; } // Reference navigation
    }
}
