namespace Eduversity.com.Shared.Dtos.CourseStructureDto
{
    public  class CourseStructureSearchResponse
    {
        public List<CourseStructureResponse> CourseStructures { get; set; } = new();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
