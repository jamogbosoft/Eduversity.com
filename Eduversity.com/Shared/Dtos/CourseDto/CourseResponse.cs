using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.CourseDto
{
    public class CourseResponse
    {
        public int Id { get; set; }
        [Required, StringLength(7, MinimumLength = 7, ErrorMessage = "Course Code must be 7 characters")]
        public string Code { get; set; } = string.Empty;
        [Required, StringLength(75, ErrorMessage = "Title should not exceed 75 characters")]
        public string Title { get; set; } = string.Empty;
        [Required, Range(1, 6, ErrorMessage = "Course Unit must be between 1 and 6")]
        public int Unit { get; set; }
        [MaxLength(1, ErrorMessage = "Only one character is allowed")]
        public string Status { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public bool IsSelected { get; set; } = false;
    }
}
