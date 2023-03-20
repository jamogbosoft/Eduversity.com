namespace Eduversity.com.Server.Services.FacultyService
{
    public class FacultyService : IFacultyService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FacultyService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Faculty>> CreateFaculty(Faculty faculty)
        {
            _context.Faculties.Add(faculty);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Faculty> { Data = faculty };
        }

        public async Task<ServiceResponse<bool>> DeleteFaculty(int facultyId)
        {
            var dbFaculty = await _context.Faculties.FindAsync(facultyId);
            if (dbFaculty == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Faculty not found."
                };
            }

            dbFaculty.IsDeleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<Faculty>>> GetAdminFaculties()
        {
            var response = new ServiceResponse<List<Faculty>>
            {
                Data = await _context.Faculties
                    .Where(f => !f.IsDeleted).OrderBy(f => f.Name)
                    .Include(f => f.Departments.Where(d => !d.IsDeleted).OrderBy(d => d.Name))
                    .ThenInclude(d => d.Options.Where(o => !o.IsDeleted).OrderBy(o => o.Name))
                    .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<Faculty>> GetAdminFaculty(int facultyId)
        {
            var response = new ServiceResponse<Faculty>();
            Faculty? faculty = null;

            var result = await _context.Faculties
                        .Include(f => f.Departments.Where(d => !d.IsDeleted).OrderBy(d => d.Name))
                        .ThenInclude(d => d.Options.Where(o => !o.IsDeleted).OrderBy(o => o.Name))
                        .FirstOrDefaultAsync(f => f.Id == facultyId && !f.IsDeleted);
            faculty = result;

            if (faculty == null)
            {
                response.Success = false;
                response.Message = "Sorry! This faculty does not exist.";
            }
            else
            {
                response.Data = faculty;
            }

            return response;
        }

        public async Task<ServiceResponse<List<FacultyReadDto>>> GetFaculties()
        {
            List<FacultyReadDto>? faculties = null;
            var result = await _context.Faculties
                                 .Where(f => f.IsActive && !f.IsDeleted)
                                 .OrderBy(f => f.Name)
                                 .ToListAsync();

            faculties = _mapper.Map<List<FacultyReadDto>>(result);

            return new ServiceResponse<List<FacultyReadDto>>
            {
                Data = faculties
            };
        }

        public async Task<ServiceResponse<FacultyReadDto>> GetFaculty(int facultyId)
        {
            FacultyReadDto? faculty = null;
            var result = await _context.Faculties
                .FirstOrDefaultAsync(f => f.Id == facultyId && f.IsActive && !f.IsDeleted);

            if (result == null)
            {
                return new ServiceResponse<FacultyReadDto>()
                {
                    Success = false,
                    Message = "Sorry! This faculty does not exist.",
                    Data = null
                };
            }

            faculty = _mapper.Map<FacultyReadDto>(result);

            return new ServiceResponse<FacultyReadDto>()
            {
                Success = false,
                Message = "Sorry! This faculty does not exist.",
                Data = faculty
            };
        }

        public async Task<ServiceResponse<Faculty>> UpdateFaculty(Faculty faculty)
        {
            var dbFaculty = await _context.Faculties.FindAsync(faculty.Id);
            if (dbFaculty == null)
            {
                return new ServiceResponse<Faculty>
                {
                    Success = false,
                    Message = "Faculty not found."
                };
            }

            dbFaculty.Name = faculty.Name;
            dbFaculty.Abbreviation = faculty.Abbreviation;
            dbFaculty.IsActive = faculty.IsActive;
            dbFaculty.IsDeleted = faculty.IsDeleted;

            foreach (var department in faculty.Departments)
            {
                var dbDepartment = await _context.Departments.FindAsync(department.Id);
                if (dbDepartment == null)
                {
                    //Add new Department
                    _context.Departments.Add(department);
                }
                else
                {
                    //Update a particular Department
                    dbDepartment.FacultyId = department.FacultyId;
                    dbDepartment.Name = department.Name;
                    dbDepartment.Abbreviation = department.Abbreviation;
                    dbDepartment.IsActive = department.IsActive;
                    dbDepartment.IsDeleted = department.IsDeleted;
                }
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<Faculty> { Data = faculty };
        }
    }
}
