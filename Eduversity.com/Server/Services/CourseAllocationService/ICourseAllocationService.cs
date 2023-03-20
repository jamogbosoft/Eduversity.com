namespace Eduversity.com.Server.Services.CourseAllocationService
{
    public interface ICourseAllocationService
    {
        Task<ServiceResponse<List<CourseAllocationResponse>>> GetCoursesAllocatedToLecturer(int lecturerId);
        Task<ServiceResponse<List<CourseAllocationResponse>>> GetCoursesAllocatedToLecturer(int lecturerId, string session);
        Task<ServiceResponse<List<CourseAllocationResponse>>> GetLecturersAllocatedToCourse(int courseId, string session);

        Task<ServiceResponse<CourseAllocationResponse>> AddCourseAllocation(CourseAllocationRequest courseAllocationRequest);
        Task<ServiceResponse<bool>> RemoveCourseAllocation(CourseAllocationRequest courseAllocationRequest);
    }
}
