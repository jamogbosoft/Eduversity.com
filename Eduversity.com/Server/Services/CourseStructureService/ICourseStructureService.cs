namespace Eduversity.com.Server.Services.CourseStructureService
{
    public interface ICourseStructureService
    {
        Task<ServiceResponse<List<CourseStructureResponse>>> GetListOfCourses(int optionId);
        Task<ServiceResponse<List<CourseStructureResponse>>> GetListOfCourses(int optionId, int level, string semester);
        Task<ServiceResponse<CourseStructureResponse>> AddCourse(CourseStructureRequest courseStructureRequest);
        Task<ServiceResponse<bool>> RemoveCourse(long structureId);
    }
}
