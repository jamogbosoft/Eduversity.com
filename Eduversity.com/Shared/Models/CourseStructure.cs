using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eduversity.com.Shared.Models
{
    public class CourseStructure
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public int CourseId { get; set; } // Foreign key 
        [Required]
        public int DepartmentOptionId { get; set; }
        [Required]
        public int Level { get; set; }
        [Required, MaxLength(6)]
        public string Semester { get; set; } = string.Empty;

        [ForeignKey(nameof(CourseId))]
        public Course? Course { get; set; }// Reference navigation
        [ForeignKey(nameof(DepartmentOptionId))]
        public DepartmentOption? DepartmentOption { get; set; } // Reference navigation
    }
}
