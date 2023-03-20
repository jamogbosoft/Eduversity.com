namespace Eduversity.com.Server.Services.DepartmentOptionService
{
    public class DepartmentOptionService : IDepartmentOptionService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DepartmentOptionService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<DepartmentOption>> CreateDepartmentOption(DepartmentOption option)
        {
            _context.DepartmentOptions.Add(option);
            await _context.SaveChangesAsync();
            return new ServiceResponse<DepartmentOption> { Data = option };
        }

        public async Task<ServiceResponse<bool>> DeleteDepartmentOption(int optionId)
        {
            var dbOption = await _context.DepartmentOptions.FindAsync(optionId);
            if (dbOption == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "Option not found."
                };
            }

            dbOption.IsDeleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<DepartmentOptionResponse>> GetAdminDepartmentOption(int optionId)
        {
            var response = new ServiceResponse<DepartmentOptionResponse>();
            DepartmentOption option;

            var result = await _context.DepartmentOptions
                          .FirstOrDefaultAsync(o => o.Id == optionId && !o.IsDeleted);
            option = result!;

            if (option == null)
            {
                response.Success = false;
                response.Message = $"Sorry! There is no option with this id '{optionId}'";
            }
            else
            {
                var item = await (from d in _context.Departments
                                  join f in _context.Faculties
                                  on d.FacultyId equals f.Id
                                  where d.Id == option.DepartmentId
                                  select new
                                  {
                                      FacultyId = f.Id,
                                      FacultyName = f.Name,
                                      DepartmentId = d.Id,
                                      DepartmentName = d.Name
                                  }).FirstOrDefaultAsync();

                if (item == null) 
                {
                    response.Success = false;
                    response.Message = $"Sorry! There is no department or faculty for this option '{option.Name}' with option Id '{option.Id}'";
                }
                else
                {
                    response.Data = new DepartmentOptionResponse()
                    {
                        FacultyId = item.FacultyId,
                        FacultyName = item.FacultyName,
                        DepartmentId = item.DepartmentId,
                        DepartmentName = item.DepartmentName,
                        Option = option
                    };
                }
            }            

            return response;
        }
       
        public async Task<ServiceResponse<DepartmentOptionsResponse>> GetAdminDepartmentOptions(int departmentId)
        {
            var options = await _context.DepartmentOptions
                     .Where(o => !o.IsDeleted && o.DepartmentId == departmentId)
                     .OrderBy(o => o.Name)
                     .ToListAsync();

            var item = await (from d in _context.Departments
                             join f in _context.Faculties
                             on d.FacultyId equals f.Id
                             where d.Id == departmentId
                             select new
                             {
                                 FacultyId = f.Id,
                                 FacultyName = f.Name,
                                 DepartmentId = d.Id,
                                 DepartmentName = d.Name
                             }).FirstOrDefaultAsync();

            if (item == null)
            {
                return new ServiceResponse<DepartmentOptionsResponse>
                {
                    Data = null,
                    Success = false,
                    Message = $"Sorry! There is no department or faculty with this department Id '{departmentId}'"

                };
            }

            var response = new ServiceResponse<DepartmentOptionsResponse>
            {
                Data = new DepartmentOptionsResponse()
                {
                    FacultyId = item.FacultyId,
                    FacultyName = item.FacultyName,
                    DepartmentId = item.DepartmentId,
                    DepartmentName = item.DepartmentName,
                    Options = options
                }
            };

            return response;
        }

        public async Task<ServiceResponse<DepartmentOptionReadDto>> GetDepartmentOption(int optionId)
        {
            DepartmentOptionReadDto? option = null;

            var result = await _context.DepartmentOptions
                .FirstOrDefaultAsync(o => o.Id == optionId && o.IsActive && !o.IsDeleted);

            if (result == null)
            {
                return new ServiceResponse<DepartmentOptionReadDto>()
                {
                    Success = false,
                    Message = $"Sorry! There is no option with this id '{optionId}'",
                    Data = null
                };
            }
            option = _mapper.Map<DepartmentOptionReadDto>(result);
            return new ServiceResponse<DepartmentOptionReadDto>()
            {
                Data = option
            };
        }

        public async Task<ServiceResponse<List<DepartmentOptionReadDto>>> GetDepartmentOptions()
        {
            List<DepartmentOptionReadDto>? options = null;

            var result = await _context.DepartmentOptions
                        .Where(o => o.IsActive && !o.IsDeleted)
                        .OrderBy(o => o.Name)
                        .ToListAsync();

            if (result == null || result.Count == 0)
            {
                return new ServiceResponse<List<DepartmentOptionReadDto>>
                {
                    Success = false,
                    Message = "Empty Record",
                    Data = null
                };
            }

            options = _mapper.Map<List<DepartmentOptionReadDto>>(result);
            return new ServiceResponse<List<DepartmentOptionReadDto>>
            {
                Data = options
            };
        }

        public async Task<ServiceResponse<List<DepartmentOptionReadDto>>> GetDepartmentOptions(int departmentId)
        {
            List<DepartmentOptionReadDto>? options = null;

            var result = await _context.DepartmentOptions
                        .Where(o => o.IsActive && !o.IsDeleted && o.DepartmentId == departmentId)
                        .OrderBy(o => o.Name)
                        .ToListAsync();

            if (result == null || result.Count == 0)
            {
                return new ServiceResponse<List<DepartmentOptionReadDto>>
                {
                    Success = false,
                    Message = $"Sorry! There is no department or faculty with this department id '{departmentId}'",
                    Data = null
                };
            }

            options = _mapper.Map<List<DepartmentOptionReadDto>>(result);
            return new ServiceResponse<List<DepartmentOptionReadDto>>
            {
                Data = options
            };
        }

        public async Task<ServiceResponse<DepartmentOption>> UpdateDepartmentOption(DepartmentOption option)
        {
            var dbOption = await _context.DepartmentOptions.FindAsync(option.Id);
            if (dbOption == null)
            {
                return new ServiceResponse<DepartmentOption>
                {
                    Success = false,
                    Message = "Option not found."
                };
            }

            dbOption.Name = option.Name;
            dbOption.Abbreviation = option.Abbreviation;
            dbOption.IsActive = option.IsActive;
            dbOption.IsDeleted = option.IsDeleted;

            await _context.SaveChangesAsync();
            return new ServiceResponse<DepartmentOption> { Data = option };
        }
    }
}
