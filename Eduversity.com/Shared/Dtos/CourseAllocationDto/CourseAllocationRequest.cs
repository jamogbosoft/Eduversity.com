namespace Eduversity.com.Shared.Dtos.CourseAllocationDto
{
    public class CourseAllocationRequest
    {
        public int LecturerId { get; set; }
        public int CourseId { get; set; }
        public string Session { get; set; } = string.Empty;
    }
}
