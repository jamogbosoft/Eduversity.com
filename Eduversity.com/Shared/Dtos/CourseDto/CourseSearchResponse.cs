namespace Eduversity.com.Shared.Dtos.CourseDto
{
    public  class CourseSearchResponse
    {
        public List<CourseResponse> Courses { get; set; } = new();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
