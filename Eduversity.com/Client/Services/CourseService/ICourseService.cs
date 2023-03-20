namespace Eduversity.com.Client.Services.CourseService
{
    public interface ICourseService
    {
        event Action CoursesChanged;
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        List<CourseResponse> Courses { get; set; }
        Task GetCourses();
        Task GetDeletedCourses();
        Task<CourseResponse> GetCourseById(int courseId);
        Task<CourseResponse> CreateCourse(CourseRequest courseRequest);
        Task<CourseResponse> UpdateCourse(int courseId, CourseRequest courseRequest);
        Task DeleteCourse(int courseId);

        Task SearchCourses(string searchText, int page);
        Task<List<string>> GetCourseSearchSuggestions(string searchText);
    }
}
