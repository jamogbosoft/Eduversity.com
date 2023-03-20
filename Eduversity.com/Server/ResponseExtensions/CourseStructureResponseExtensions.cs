namespace Eduversity.com.Server.ResponseExtensions
{
    public static class CourseStructureResponseExtensions
    {
        public static IQueryable<CourseStructureResponse> ToCourseStructureResponse(this IQueryable<CourseStructure> source)
        {
            var respomse = source
                .Select(cs => new CourseStructureResponse
                {
                    Id = cs.Id,
                    Level = cs.Level,
                    Semester = cs.Semester,
                    CourseId = cs.CourseId,
                    CourseCode = cs.Course!.Code,
                    CourseTitle = cs.Course.Title,
                    CourseUnit = cs.Course.Unit,
                    CourseStatus = cs.Course.Status,

                    DepartmentOptionId = cs.DepartmentOption!.Id,
                    DepartmentOptionName = cs.DepartmentOption.Name,

                    DepartmentId = cs.DepartmentOption.Department!.Id,
                    DepartmentName = cs.DepartmentOption.Department.Name,

                    FacultyId = cs.DepartmentOption.Department.Faculty!.Id,
                    FacultyName = cs.DepartmentOption.Department.Faculty.Name
                });
            return respomse;
        }
    }
}
