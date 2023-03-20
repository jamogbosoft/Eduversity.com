using Eduversity.com.Server.ResponseExtensions;
using Eduversity.com.Shared.Dtos.StudentDto;
using Eduversity.com.Shared.Models;
using Microsoft.Extensions.Options;

namespace Eduversity.com.Server.Services.LecturerService
{
    public class LecturerService : ILecturerService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public LecturerService(DataContext context, IAuthService authService, IMapper mapper)
        {
            _context = context;
            _authService = authService;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<LecturerResponse>> AddOrUpdateLecturer(LecturerResponse lecturerResponse)
        {
            long userId = lecturerResponse.UserId == 0 ? _authService.GetUserId() : lecturerResponse.UserId;
            var dbLecturer = await _context.Lecturers
                .Include(l => l.AcademicDetail)
                .ThenInclude(a => a.Qualifications)
                .FirstOrDefaultAsync(l => l.UserId == userId);

            if (dbLecturer is null)
            {
                //Add Lecturer
                Lecturer lecturer = _mapper.Map<Lecturer>(lecturerResponse);
                lecturer.UserId = userId;

                _context.Lecturers.Add(lecturer);
            }
            else
            {
                //Update Lecturer
                dbLecturer.Name = lecturerResponse.Name;
                dbLecturer.Gender = lecturerResponse.Gender;
                dbLecturer.MaritalStatus = lecturerResponse.MaritalStatus;
                dbLecturer.Image = lecturerResponse.Image;

                if (dbLecturer.AcademicDetail is not null)
                {
                    dbLecturer.AcademicDetail.DepartmentId = lecturerResponse.AcademicDetail.DepartmentId;
                    dbLecturer.AcademicDetail.Designation = lecturerResponse.AcademicDetail.Designation;
                    dbLecturer.AcademicDetail.Specialisation = lecturerResponse.AcademicDetail.Specialisation;
                    dbLecturer.AcademicDetail.Certifications = lecturerResponse.AcademicDetail.Certifications;
                    dbLecturer.AcademicDetail.Activities = lecturerResponse.AcademicDetail.Activities;
                    dbLecturer.AcademicDetail.ProfessionalAffiliation = lecturerResponse.AcademicDetail.ProfessionalAffiliation;
                    dbLecturer.AcademicDetail.Award = lecturerResponse.AcademicDetail.Award;
                    dbLecturer.AcademicDetail.Memo = lecturerResponse.AcademicDetail.Memo;

                    foreach (var qualificationResponse in lecturerResponse.AcademicDetail.Qualifications)
                    {
                        var qualification = dbLecturer.AcademicDetail.Qualifications.Find(q => q.Id == qualificationResponse.Id);
                        if (qualification is null)
                        {
                            //Add New Qualification
                            Qualification newQualification = _mapper.Map<Qualification>(qualificationResponse);
                            
                             _context.Qualifications.Add(newQualification);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            //Update Qualification
                            if (qualificationResponse.IsDeleted)
                            {
                                dbLecturer.AcademicDetail.Qualifications.Remove(qualification);
                            }
                            else
                            {
                                qualification.Degree = qualificationResponse.Degree;
                                qualification.CourseOfStudy = qualificationResponse.CourseOfStudy;
                                qualification.Institution = qualificationResponse.Institution;
                                qualification.YearGraduated = qualificationResponse.YearGraduated;
                            }
                        }
                    }
                }
            }

            await _context.SaveChangesAsync();

            var response = await GetLecturer(userId);
            return new ServiceResponse<LecturerResponse>()
            {
                Data = response.Data
            };
        }

        public async Task<ServiceResponse<bool>> DeleteLecturer(int lecturerId)
        {
            var dbLecturer = await _context.Lecturers
                .Include(l => l.AcademicDetail)
                .ThenInclude(a => a.Qualifications)
                .FirstOrDefaultAsync(l => l.Id == lecturerId);

            if (dbLecturer is null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "This lecturer does not exist."
                };
            }
            _context.Lecturers.Remove(dbLecturer);

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>() { Data = true };
        }

        public async Task<ServiceResponse<LecturerResponse>> GetLecturer(long Id = 0L)
        {
            var userId = Id == 0L ? _authService.GetUserId() : Id;
            // Using extension method
            var lecturer = await _context.Lecturers
                .ToLecturerResponse()
                .FirstOrDefaultAsync(l => l.UserId == userId);

            var response = new ServiceResponse<LecturerResponse>
            {
                Data = lecturer
            };

            return response;
        }

        public async Task<ServiceResponse<List<LecturerResponse>>> GetLecturers()
        {
            // Using extension method
            var lecturers = await _context.Lecturers.ToLecturerResponse().ToListAsync();

            var response = new ServiceResponse<List<LecturerResponse>>
            {
                Data = lecturers
            };

            return response;
        }

        public async Task<ServiceResponse<List<LecturerResponse>>> GetLecturers(int departmentId)
        {
            // Using extension method
            var lecturers = await _context.Lecturers
                .Where(l => l.AcademicDetail.DepartmentId == departmentId)
                .ToLecturerResponse()
                .ToListAsync();

            var response = new ServiceResponse<List<LecturerResponse>>
            {
                Data = lecturers
            };

            return response;
        }

        public async Task<ServiceResponse<List<string>>> GetLecturerSearchSuggestions(string searchText, int departmentId)
        {
            var lecturers = await FindLecturersBySearchText(searchText, departmentId);

            List<string> result = new List<string>();

            foreach (var lecturer in lecturers)
            {
                if (lecturer.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    && !result.Contains(lecturer.Name))
                {
                    result.Add(lecturer.Name);
                }
                if (lecturer.AcademicDetail.FacultyName.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                    && !result.Contains(lecturer.AcademicDetail.FacultyName))
                {
                    result.Add(lecturer.AcademicDetail.FacultyName);
                }
                if (lecturer.AcademicDetail.DepartmentName.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                     && !result.Contains(lecturer.AcademicDetail.DepartmentName))
                {
                    result.Add(lecturer.AcademicDetail.DepartmentName);
                }
            }
            return new ServiceResponse<List<string>> { Data = result };
        }

        public async Task<ServiceResponse<LecturerSearchResponse>> SearchLecturers(string searchText, int page, int departmentId, int pageSize)
        {
            var result = await FindLecturersBySearchText(searchText, departmentId);

            var rocordCount = (float)pageSize;
            var pageCount = Math.Ceiling(result.Count / rocordCount);
            var lecturers = result
                    .Skip((page - 1) * (int)rocordCount)
                    .Take((int)rocordCount)
            .ToList();

            var response = new ServiceResponse<LecturerSearchResponse>
            {
                Data = new LecturerSearchResponse
                {
                    Lecturers = lecturers,
                    CurrentPage = page,
                    Pages = (int)pageCount
                }
            };

            return response;
        }

        private async Task<List<LecturerResponse>> FindLecturersBySearchText(string searchText, int departmentId)
        {
            List<LecturerResponse>? lecturers;
            if (departmentId == 0)
            {
                 lecturers = await _context.Lecturers
                 .ToLecturerResponse()
                 .Where(l => l.Name.ToLower().Contains(searchText.ToLower()) ||
                    l.AcademicDetail.DepartmentName.ToLower().Contains(searchText.ToLower()) ||
                    l.AcademicDetail.FacultyName.ToLower().Contains(searchText.ToLower()))
                 .ToListAsync();
            }
            else
            {
                lecturers = await _context.Lecturers                
                 .ToLecturerResponse()
                 .Where(l => (l.Name.ToLower().Contains(searchText.ToLower()) ||
                    l.AcademicDetail.DepartmentName.ToLower().Contains(searchText.ToLower()) ||
                    l.AcademicDetail.FacultyName.ToLower().Contains(searchText.ToLower())) &&
                    l.AcademicDetail.DepartmentId == departmentId)
                 .ToListAsync();
            }
            return lecturers;

        }
    }
}
