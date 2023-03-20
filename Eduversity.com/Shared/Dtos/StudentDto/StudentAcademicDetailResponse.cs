using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.StudentDto
{
    public class StudentAcademicDetailResponse
    {

        public long StudentId { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "RegNumber should not exceed 20 characters.")]
        public string RegNumber { get; set; } = string.Empty;
        public int FacultyId { get; set; }
        public int DepartmentId { get; set; }
        public int DepartmentOptionId { get; set; }
        [Required]
        [MaxLength(9, ErrorMessage = "Session should not exceed 9 characters.")]
        public string Session { get; set; } = string.Empty;
        [Required]
        public int Level { get; set; }
        [Required]
        [MaxLength(6, ErrorMessage = "Semester should not exceed 6 characters.")]
        public string Semester { get; set; } = string.Empty;
        public bool Graduated { get; set; } = false;
        public bool PassedOut { get; set; } = false;
        public string Memo { get; set; } = string.Empty;
        public string FacultyName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string DepartmentOptionName { get; set; } = string.Empty;
    }
}
