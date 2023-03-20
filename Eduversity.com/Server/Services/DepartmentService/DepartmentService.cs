namespace Eduversity.com.Server.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DepartmentService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<Department>> CreateDepartment(Department department)
        {
            _context.Departments.Add(department);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Department> { Data = department };
        }

        public async Task<ServiceResponse<bool>> DeleteDepartment(int departmentId)
        {
            var dbDepartment = await _context.Departments.FindAsync(departmentId);
            if (dbDepartment == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Department not found."
                };
            }

            dbDepartment.IsDeleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<DepartmentResponse>> GetAdminDepartment(int departmentId)
        {
            var response = new ServiceResponse<DepartmentResponse>();
            Department department;

            var result = await _context.Departments
                        .Include(d => d.Options.Where(o => !o.IsDeleted).OrderBy(o => o.Name))
                        .FirstOrDefaultAsync(d => d.Id == departmentId && !d.IsDeleted);
            department = result!;

            if (department == null)
            {
                response.Success = false;
                response.Message = $"Sorry! There is no department with this id '{departmentId}'";
            }
            else
            {
                var faculty = await _context.Faculties.FindAsync(department.FacultyId);
                response.Data = new DepartmentResponse()
                {
                    FacultyId = faculty!.Id,
                    FacultyName = faculty!.Name,
                    Department = department
                };
            }

            return response;
        }

        public async Task<ServiceResponse<DepartmentsResponse>> GetAdminDepartments(int facultyId)
        {
            var department = await _context.Departments
                     .Where(d => !d.IsDeleted && d.FacultyId == facultyId).OrderBy(d => d.Name)
                     .Include(d => d.Options.Where(o => !o.IsDeleted).OrderBy(o => o.Name))
                     .ToListAsync();
            var faculty = await _context.Faculties.FindAsync(facultyId);

            if (faculty == null)
            {
                return new ServiceResponse<DepartmentsResponse>
                {
                    Data = null,
                    Success = false,
                    Message = $"There is no faculty with this id '{facultyId}'"
                };
            }

            var response = new ServiceResponse<DepartmentsResponse>
            {
                Data = new DepartmentsResponse()
                {
                    FacultyId= faculty.Id,
                    FacultyName= faculty.Name,
                    Departments= department
                }
            };

            return response;
        }

        public async Task<ServiceResponse<DepartmentReadDto>> GetDepartment(int departmentId)
        {
            DepartmentReadDto? department = null;

            var result = await _context.Departments
                .FirstOrDefaultAsync(d => d.Id == departmentId && d.IsActive && !d.IsDeleted);

            if (result == null)
            {
                return new ServiceResponse<DepartmentReadDto>()
                {
                    Success = false,
                    Message = $"Sorry! There is no department with this id '{departmentId}'",
                    Data = null
                };
            }

            department = _mapper.Map<DepartmentReadDto>(result);
            return new ServiceResponse<DepartmentReadDto>()
            {
                Data = department
            };
        }

        public async Task<ServiceResponse<List<DepartmentReadDto>>> GetDepartments(int facultyId)
        {
            List<DepartmentReadDto>? department = null;
            var result = await _context.Departments
                        .Where(d => d.IsActive && !d.IsDeleted && d.FacultyId == facultyId)
                        .OrderBy(d => d.Name)
                        .ToListAsync();

            if (result == null || result.Count == 0)
            {
                return new ServiceResponse<List<DepartmentReadDto>>()
                {
                    Success = false,
                    Message = $"Sorry! There is no department with this faculty id '{facultyId}'",
                    Data = null
                };
            }

            department = _mapper.Map<List<DepartmentReadDto>>(result);
            return new ServiceResponse<List<DepartmentReadDto>>
            {
                Data = department
            };
        }

        public async Task<ServiceResponse<Department>> UpdateDepartment(Department department)
        {
            var dbDepartment = await _context.Departments.FindAsync(department.Id);
            if (dbDepartment == null)
            {
                return new ServiceResponse<Department>
                {
                    Success = false,
                    Message = "Department not found."
                };
            }

            dbDepartment.Name = department.Name;
            dbDepartment.Abbreviation = department.Abbreviation;
            dbDepartment.IsActive = department.IsActive;
            dbDepartment.IsDeleted = department.IsDeleted;

            foreach (var option in department.Options)
            {
                var dbOption = await _context.DepartmentOptions.FindAsync(option.Id);
                if (dbOption == null)
                {
                    //Add new DepartmentOption
                    _context.DepartmentOptions.Add(option);
                }
                else
                {
                    //Update a particular DepartmentOption
                    dbOption.DepartmentId = option.DepartmentId;
                    dbOption.Name = option.Name;
                    dbOption.Abbreviation = option.Abbreviation;
                    dbOption.IsActive = option.IsActive;
                    dbOption.IsDeleted = option.IsDeleted;
                }
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<Department> { Data = department };
        }
    }
}