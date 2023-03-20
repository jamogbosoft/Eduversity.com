using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.CourseDto
{
    public class CourseRequest
    {
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Unit { get; set; }
        public string Status { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}
