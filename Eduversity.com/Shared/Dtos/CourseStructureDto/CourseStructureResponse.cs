using Eduversity.com.Shared.Models;

namespace Eduversity.com.Shared.Dtos.CourseStructureDto
{
    public class CourseStructureResponse
    {
        public long Id { get; set; }
        public int Level { get; set; }
        public string Semester { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public string CourseCode { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public int CourseUnit { get; set; }
        public string CourseStatus { get; set; } = string.Empty;

        public int FacultyId { get; set; }
        public int DepartmentId { get; set; }
        public int DepartmentOptionId { get; set; }
        public string FacultyName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string DepartmentOptionName { get; set; } = string.Empty;
    }
}