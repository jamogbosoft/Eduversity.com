using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.CourseStructureDto
{
    public class CourseStructureRequest
    {
        public int CourseId { get; set; }
        public int DepartmentOptionId { get; set; }
        public int Level { get; set; }
        [Required]
        [MaxLength(6, ErrorMessage = "Semester should not exceed 6 characters.")]
        public string Semester { get; set; } = string.Empty;
    }
}
