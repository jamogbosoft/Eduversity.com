using Eduversity.com.Server.ResponseExtensions;

namespace Eduversity.com.Server.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public StudentService(DataContext context, IAuthService authService, IMapper mapper)
        {
            _context = context;
            _authService = authService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<StudentResponse>>> GetStudents()
        {
            //var students =
            //    await (from st in _context.Students
            //           join ac in _context.StudentsAcademicDetail
            //           on st.UserId equals ac.UserId
            //           join o in _context.DepartmentOptions
            //           on ac.DepartmentOptionId equals o.Id into acGroup
            //           from o in acGroup.DefaultIfEmpty()
            //           join d in _context.Departments
            //           on o.DepartmentId equals d.Id into oGroup
            //           from d in oGroup.DefaultIfEmpty()
            //           join f in _context.Faculties
            //           on d.FacultyId equals f.Id into dGroup
            //           from f in dGroup.DefaultIfEmpty()
            //           orderby f.Name, d.Name, o.Name,
            //                   ac.Session, ac.Level,
            //                   st.LastName, st.FirstName, st.MiddleName
            //           select new StudentResponse
            //           {
            //               UserId = st.UserId,
            //               FirstName = st.FirstName,
            //               MiddleName = st.MiddleName,
            //               LastName = st.LastName,
            //               DateOfBirth = st.DateOfBirth,
            //               PlaceOfBirth = st.PlaceOfBirth,
            //               Gender = st.Gender,
            //               MaritalStatus = st.MaritalStatus,
            //               //ImageUrl =
            //               AcademicDetail = new StudentAcademicDetailResponse
            //               {
            //                   UserId = ac.UserId,
            //                   RegNumber = ac.RegNumber,
            //                   FacultyId = f != null ? f.Id : 0,
            //                   FacultyName = f != null ? f.Name : string.Empty,
            //                   DepartmentId = d != null ? d.Id : 0,
            //                   DepartmentName = d != null ? d.Name : string.Empty,
            //                   DepartmentOptionId = o != null ? o.Id : 0,
            //                   DepartmentOptionName = o != null ? o.Name : string.Empty,
            //                   Session = ac.Session,
            //                   Level = ac.Level,
            //                   Semester = ac.Semester,
            //                   Graduated = ac.Graduated,
            //                   PassedOut = ac.PassedOut,
            //                   Memo = ac.Memo
            //               }
            //           }).ToListAsync();


            // Using extension method
            var students = await _context.Students.ToStudentResponse().ToListAsync(); 

            var response = new ServiceResponse<List<StudentResponse>>
            {
                Data = students
            };

            return response;
        }

        public async Task<ServiceResponse<List<StudentResponse>>> GetStudents(int optionId)
        {
            // Using extension method
            var students = await _context.Students                
                .Where(s => s.AcademicDetail.DepartmentOptionId == optionId)
                .ToStudentResponse()
                .ToListAsync();

            var response = new ServiceResponse<List<StudentResponse>>
            {
                Data = students
            };

            return response;
        }

        public async Task<ServiceResponse<List<StudentResponse>>> GetStudents(int optionId, string Session, int Level)
        {
            // Using extension method
            var students = await _context.Students
                .Where(s => s.AcademicDetail.DepartmentOptionId == optionId)
                .Where(s => s.AcademicDetail.Session == Session)
                .Where(s => s.AcademicDetail.Level == Level)
                .ToStudentResponse()
                .ToListAsync();

            var response = new ServiceResponse<List<StudentResponse>>
            {
                Data = students
            };

            return response;
        }

        public async Task<ServiceResponse<StudentResponse>> GetStudent(long Id = 0L)
        {
            var userId = Id == 0L ? _authService.GetUserId() : Id;
            // Using extension method
            var student = await _context.Students
                .ToStudentResponse()
                .FirstOrDefaultAsync(s => s.UserId == userId);

            var response = new ServiceResponse<StudentResponse>
            {
                Data = student
            };
            return response;
        }

        public async Task<ServiceResponse<StudentResponse>> AddOrUpdateStudent(StudentResponse studentResponse)
        {
            long userId = studentResponse.UserId == 0 ? _authService.GetUserId() : studentResponse.UserId;
            var dbStudent = await _context.Students
                            .Include(s => s.AcademicDetail)
                            .FirstOrDefaultAsync(s => s.UserId == userId);

            if (dbStudent == null)
            {
                //Add Student
                Student student = _mapper.Map<Student>(studentResponse);
                student.UserId = userId;

                if(student.AcademicDetail.RegNumber == string.Empty)
                {
                    student.AcademicDetail.RegNumber = userId.ToString();
                }                    
               _context.Students.Add(student);
            }
            else
            {
                //Update Student
                dbStudent.FirstName = studentResponse.FirstName;
                dbStudent.MiddleName = studentResponse.MiddleName;
                dbStudent.LastName = studentResponse.LastName;
                dbStudent.DateOfBirth = studentResponse.DateOfBirth;
                dbStudent.PlaceOfBirth = studentResponse.PlaceOfBirth;
                dbStudent.Gender = studentResponse.Gender;
                dbStudent.MaritalStatus = studentResponse.MaritalStatus;
                dbStudent.Image = studentResponse.Image;

                if (dbStudent.AcademicDetail != null)
                {
                    dbStudent.AcademicDetail.RegNumber = studentResponse.AcademicDetail.RegNumber;
                    dbStudent.AcademicDetail.DepartmentOptionId = studentResponse.AcademicDetail.DepartmentOptionId;
                    dbStudent.AcademicDetail.Session = studentResponse.AcademicDetail.Session;
                    dbStudent.AcademicDetail.Level = studentResponse.AcademicDetail.Level;
                    dbStudent.AcademicDetail.Semester = studentResponse.AcademicDetail.Semester;
                    dbStudent.AcademicDetail.Graduated = studentResponse.AcademicDetail.Graduated;
                    dbStudent.AcademicDetail.PassedOut = studentResponse.AcademicDetail.PassedOut;
                    dbStudent.AcademicDetail.Memo = studentResponse.AcademicDetail.Memo;
                }
            }

            await _context.SaveChangesAsync();

            var response = await GetStudent(userId);
            return new ServiceResponse<StudentResponse>()
            {
                Data = response.Data
            };
        }

        public async Task<ServiceResponse<bool>> DeleteStudent(long studentId)
        {
            var dbStudent = await _context.Students
                        .Include(s => s.AcademicDetail)
                        .FirstOrDefaultAsync(s => s.Id == studentId);

            if (dbStudent == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "This student does not exist."
                };
            }

            _context.Students.Remove(dbStudent);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>() { Data = true };
        }

        public async Task<ServiceResponse<List<string>>> GetStudentSearchSuggestions(string searchText, int optionId)
        {
            var students = await FindStudentsBySearchText(searchText, optionId);

            List<string> result = new List<string>();

            foreach (var student in students)
            {

                if (student.FirstName.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    && !result.Contains(student.FirstName))
                {
                    result.Add(student.FirstName);
                }
                if (student.MiddleName.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    && !result.Contains(student.MiddleName))
                {
                    result.Add(student.MiddleName);
                }
                if (student.LastName.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    && !result.Contains(student.LastName))
                {
                    result.Add(student.LastName);
                }
                if (student.AcademicDetail.Level.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    && !result.Contains(student.AcademicDetail.Level.ToString()))
                {
                    result.Add(student.AcademicDetail.Level.ToString());
                }
                if (student.AcademicDetail.FacultyName.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    && !result.Contains(student.AcademicDetail.FacultyName))
                {
                    result.Add(student.AcademicDetail.FacultyName);
                }
                if (student.AcademicDetail.DepartmentName.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                     && !result.Contains(student.AcademicDetail.DepartmentName))
                {
                    result.Add(student.AcademicDetail.DepartmentName);
                }
                if (student.AcademicDetail.DepartmentOptionName.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    && !result.Contains(student.AcademicDetail.DepartmentOptionName))
                {
                    result.Add(student.AcademicDetail.DepartmentOptionName);
                }
            }
            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<StudentSearchResponse>> SearchStudents(string searchText, int page, int optionId, int pageSize)
        {
            var result = await FindStudentsBySearchText(searchText, optionId);

            var rocordCount = (float)pageSize;
            var pageCount = Math.Ceiling(result.Count / rocordCount);
            var students = result
                    .Skip((page - 1) * (int)rocordCount)
                    .Take((int)rocordCount)
                    .ToList();

            var response = new ServiceResponse<StudentSearchResponse>
            {
                Data = new StudentSearchResponse
                {
                    Students = students,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        private async Task<List<StudentResponse>> FindStudentsBySearchText(string searchText , int optionId)
        {
            List<StudentResponse>? students;
            if (optionId == 0)
            {
                students = await _context.Students
                     .ToStudentResponse()
                     .Where(s => s.FirstName.ToLower().Contains(searchText.ToLower()) ||
                        s.MiddleName.ToLower().Contains(searchText.ToLower()) ||
                        s.LastName.ToLower().Contains(searchText.ToLower()) ||
                        s.AcademicDetail.Level.ToString().ToLower().Contains(searchText.ToLower()) ||
                        s.AcademicDetail.DepartmentOptionName.ToLower().Contains(searchText.ToLower()) ||
                        s.AcademicDetail.DepartmentName.ToLower().Contains(searchText.ToLower()) ||
                        s.AcademicDetail.FacultyName.ToLower().Contains(searchText.ToLower()))
                    .ToListAsync();
            }
            else
            {
                students = await _context.Students
                    .ToStudentResponse()
                    .Where(s => (s.FirstName.ToLower().Contains(searchText.ToLower()) ||
                       s.MiddleName.ToLower().Contains(searchText.ToLower()) ||
                       s.LastName.ToLower().Contains(searchText.ToLower()) ||
                       s.AcademicDetail.Level.ToString().ToLower().Contains(searchText.ToLower()) ||
                       s.AcademicDetail.DepartmentOptionName.ToLower().Contains(searchText.ToLower()) ||
                       s.AcademicDetail.DepartmentName.ToLower().Contains(searchText.ToLower()) ||
                       s.AcademicDetail.FacultyName.ToLower().Contains(searchText.ToLower())) &&
                       s.AcademicDetail.DepartmentOptionId == optionId)
                   .ToListAsync();
            }

            return students;
        }
    }
}