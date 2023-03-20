using System.ComponentModel.DataAnnotations;

namespace Eduversity.com.Shared.Dtos.CourseAllocationDto
{
    public  class CourseAllocationResponse
    {        
        public int LecturerId { get; set; }
        public int CourseId { get; set; }
        public string Session { get; set; } = string.Empty;

        public string CourseCode { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public int CourseUnit { get; set; }   
        
        public string LecturerName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

    }
}
