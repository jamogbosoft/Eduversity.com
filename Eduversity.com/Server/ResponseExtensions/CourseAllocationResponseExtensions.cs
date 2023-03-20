using Eduversity.com.Shared.Dtos.CourseAllocationDto;

namespace Eduversity.com.Server.ResponseExtensions
{
    public static class CourseAllocationResponseExtensions
    {
        public static IQueryable<CourseAllocationResponse> ToCourseAllocationResponse(this IQueryable<CourseAllocation> source)
        {
            var respomse = source
                .Select(ca => new CourseAllocationResponse
                {
                    CourseId = ca.CourseId,
                    LecturerId = ca.LecturerId,
                    Session = ca.Session,

                    CourseCode = ca.Course!.Code,
                    CourseTitle = ca.Course.Title,
                    CourseUnit = ca.Course.Unit,

                    LecturerName = ca.Lecturer!.Name,
                    Department = ca.Lecturer.AcademicDetail!.Department!.Name
                });
            return respomse;
        }
    }
}
