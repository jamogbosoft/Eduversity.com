namespace Eduversity.com.Server.ResponseExtensions
{
    public static class StudentResponseExtensions
    {
        public static IQueryable<StudentResponse> ToStudentResponse(this IQueryable<Student> source)
        {
            return source.Select(s => new StudentResponse
            {
                Id = s.Id,
                UserId = s.UserId,
                FirstName = s.FirstName,
                MiddleName = s.MiddleName,
                LastName = s.LastName,
                DateOfBirth = s.DateOfBirth,
                PlaceOfBirth = s.PlaceOfBirth,
                Gender = s.Gender,
                MaritalStatus = s.MaritalStatus,
                Image = s.Image,
                AcademicDetail = new StudentAcademicDetailResponse
                {
                    StudentId = s.AcademicDetail.StudentId,
                    RegNumber = s.AcademicDetail.RegNumber,

                    /////////////////////////////////////////////////////////////////////////
                    //DepartmentOptionId = s.AcademicDetail.DepartmentOption != null ?  s.AcademicDetail.DepartmentOption.Id : 0 ,
                    //DepartmentOptionName = s.AcademicDetail.DepartmentOption != null ? s.AcademicDetail.DepartmentOption.Name : string.Empty,

                    //DepartmentId = s.AcademicDetail.DepartmentOption!.Department != null ? s.AcademicDetail.DepartmentOption.Department.Id : 0,
                    //DepartmentName = s.AcademicDetail.DepartmentOption.Department != null ? s.AcademicDetail.DepartmentOption.Department.Name : string.Empty,

                    //FacultyId = s.AcademicDetail.DepartmentOption.Department!.Faculty != null ? s.AcademicDetail.DepartmentOption.Department.Faculty.Id : 0,
                    //FacultyName = s.AcademicDetail.DepartmentOption.Department.Faculty != null ? s.AcademicDetail.DepartmentOption.Department.Faculty.Name : string.Empty,

                    /////////////////////////////////////////////////////////////////////////
                    DepartmentOptionId = s.AcademicDetail.DepartmentOption!.Id,
                    DepartmentOptionName = s.AcademicDetail.DepartmentOption.Name,

                    DepartmentId = s.AcademicDetail.DepartmentOption.Department!.Id,
                    DepartmentName = s.AcademicDetail.DepartmentOption.Department.Name,

                    FacultyId = s.AcademicDetail.DepartmentOption.Department.Faculty!.Id,
                    FacultyName = s.AcademicDetail.DepartmentOption.Department.Faculty.Name,
                    /////////////////////////////////////////////////////////////////////////

                    Session = s.AcademicDetail.Session,
                    Level = s.AcademicDetail.Level,
                    Semester = s.AcademicDetail.Semester,
                    Graduated = s.AcademicDetail.Graduated,
                    PassedOut = s.AcademicDetail.PassedOut,
                    Memo = s.AcademicDetail.Memo
                }
            });
        }
    }
}
