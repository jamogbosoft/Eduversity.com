using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.LecturerDto
{
    public class QualificationResponse
    {
        public int Id { get; set; }
        public int LecturerId { get; set; }
        [Required, MaxLength(10, ErrorMessage = "Degree Type should not exceed 10 characters(eg. Ph.D, M.Sc., B.Sc., B.Tech. etc. ")]
        public string Degree { get; set; } = string.Empty;
        [Required, MaxLength(50, ErrorMessage = "Course of Study should not exceed 50 characters.")]
        public string CourseOfStudy { get; set; } = string.Empty;
        [Required, MaxLength(70, ErrorMessage = "Institution Name should not exceed 70 characters.")]
        public string Institution { get; set; } = string.Empty;
        [Required, MaxLength(4, ErrorMessage = "Year Graduated should not exceed 4 characters.")]
        public string YearGraduated { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;
    }
}