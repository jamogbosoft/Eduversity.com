namespace Eduversity.com.Client.Services.CourseStructureService
{
    public interface ICourseStructureService
    {
        event Action ListOfCoursesChanged;
        string Message { get; set; }
        List<CourseStructureResponse> ListOfCourses { get; set; }
        Task GetListOfCourses(int optionId);
        Task GetListOfCourses(int optionId, int level, string semester);
        Task<CourseStructureResponse> AddCourse(CourseStructureRequest courseStructureRequest);
        Task RemoveCourse(long structureId);
    }
}
