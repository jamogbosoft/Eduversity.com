namespace Eduversity.com.Server.Services.CourseService
{
    public interface ICourseService
    {
        Task<ServiceResponse<List<CourseResponse>>> GetCourses();
        Task<ServiceResponse<CourseResponse>> GetCourseById(int courseId);
        Task<ServiceResponse<List<CourseResponse>>> GetDeletedCourses();
        Task<ServiceResponse<CourseResponse>> CreateCourse(CourseRequest courseRequest);
        Task<ServiceResponse<CourseResponse>> UpdateCourse(int courseId, CourseRequest courseRequest);
        Task<ServiceResponse<bool>> DeleteCourse(int courseId);
        Task<ServiceResponse<CourseSearchResponse>> SearchCourses(string searchText, int page, int pageSize = 10); //set pageSize to 10, 15, or 20 later
        Task<ServiceResponse<List<string>>> GetCourseSearchSuggestions(string searchText);
    }
}
