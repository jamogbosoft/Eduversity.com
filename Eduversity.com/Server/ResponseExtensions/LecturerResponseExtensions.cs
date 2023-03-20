namespace Eduversity.com.Server.ResponseExtensions
{
    public static class LecturerResponseExtensions
    {
        public static IQueryable<LecturerResponse> ToLecturerResponse(this IQueryable<Lecturer> source)
        {
            return source.Select(l => new LecturerResponse
            {
                Id = l.Id,
                UserId = l.UserId,
                Name = l.Name,
                Gender = l.Gender,
                MaritalStatus = l.MaritalStatus,
                Image = l.Image,
                AcademicDetail = new LecturerAcademicDetailResponse
                {
                    LecturerId = l.AcademicDetail.LecturerId,

                    DepartmentId = l.AcademicDetail.Department!.Id,
                    DepartmentName = l.AcademicDetail.Department.Name,

                    FacultyId = l.AcademicDetail.Department.Faculty!.Id,
                    FacultyName = l.AcademicDetail.Department.Faculty.Name,

                    Designation = l.AcademicDetail.Designation,
                    Specialisation = l.AcademicDetail.Specialisation,
                    Certifications = l.AcademicDetail.Certifications,
                    Activities = l.AcademicDetail.Activities,
                    ProfessionalAffiliation = l.AcademicDetail.ProfessionalAffiliation,
                    Award = l.AcademicDetail.Award,
                    Memo = l.AcademicDetail.Memo,
                    Qualifications = (List<QualificationResponse>)l.AcademicDetail.Qualifications.Select(q => new QualificationResponse
                    {
                        Id = q.Id,
                        LecturerId = q.LecturerId,
                        Degree = q.Degree,
                        CourseOfStudy = q.CourseOfStudy,
                        Institution = q.Institution,
                        YearGraduated = q.YearGraduated
                    })
                }
            });
        }
    }
}
