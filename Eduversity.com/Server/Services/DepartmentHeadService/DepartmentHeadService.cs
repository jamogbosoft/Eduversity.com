using Eduversity.com.Server.ResponseExtensions;
using Eduversity.com.Shared.Dtos.DepartmentHeadDto;

namespace Eduversity.com.Server.Services.DepartmentHeadService
{
    public class DepartmentHeadService : IDepartmentHeadService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public DepartmentHeadService(DataContext context, IAuthService authService, IMapper mapper)
        {
            _context = context;
            _authService = authService;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHead()
        {
            var userId = _authService.GetUserId();
            // Using extension method
            var head = await _context.DepartmentHeads
                .ToDepartmentHeadResponse()
                .FirstOrDefaultAsync(h => h.UserId == userId);

            var response = new ServiceResponse<DepartmentHeadResponse>
            {
                Data = head
            };

            return response;
        }

        public async Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByUserId(long userId)
        {
            // Using extension method
            var head = await _context.DepartmentHeads
                .ToDepartmentHeadResponse()
                .FirstOrDefaultAsync(h => h.UserId == userId);

            var response = new ServiceResponse<DepartmentHeadResponse>
            {
                Data = head
            };

            return response;
        }

        public async Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByDepartmentId(int departmentId)
        {
            // Using extension method
            var head = await _context.DepartmentHeads
                .ToDepartmentHeadResponse()
                .FirstOrDefaultAsync(h => h.DepartmentId == departmentId);

            var response = new ServiceResponse<DepartmentHeadResponse>
            {
                Data = head
            };

            return response;
        }

        public async Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByHeadId(int headId)
        {
            // Using extension method
            var head = await _context.DepartmentHeads
                .ToDepartmentHeadResponse()
                .FirstOrDefaultAsync(h => h.Id == headId);

            var response = new ServiceResponse<DepartmentHeadResponse>
            {
                Data = head
            };

            return response;
        }

        public async Task<ServiceResponse<List<DepartmentHeadResponse>>> GetListOfDepartmentHeads()
        {
            // Using extension method
            var heads = await _context.DepartmentHeads
                .ToDepartmentHeadResponse()
                .ToListAsync();

            var response = new ServiceResponse<List<DepartmentHeadResponse>>
            {
                Data = heads
            };

            return response;
        }

        public async Task<ServiceResponse<DepartmentHeadResponse>> AddDepartmentHead(DepartmentHeadRequest request)
        {
            //Add Department Head
            DepartmentHead head = _mapper.Map<DepartmentHead>(request);
            _context.DepartmentHeads.Add(head);
            await _context.SaveChangesAsync();

            var response = await GetDepartmentHeadByUserId(request.UserId);
            return new ServiceResponse<DepartmentHeadResponse>()
            {
                Data = response.Data
            };
        }

        public async Task<ServiceResponse<DepartmentHeadResponse>> UpdateDepartmentHead(int headId, DepartmentHeadRequest request)
        {
            var dbHead = await _context.DepartmentHeads
                .FirstOrDefaultAsync(h => h.Id == headId);

            if (dbHead is null)
            {
                return new ServiceResponse<DepartmentHeadResponse>
                {
                    Success = false,
                    Message = "Department head not found."
                };
            }

            ///dbHead.UserId = request.UserId;             Don't re-allocate another account to this department   
            ///dbHead.DepartmentId = request.DepartmentId; Don't re-allocate this account to another department
            dbHead.LecturerId = request.LecturerId;      //Update only the new head
            await _context.SaveChangesAsync();

            var response = await GetDepartmentHeadByHeadId(headId);
            return new ServiceResponse<DepartmentHeadResponse>()
            {
                Data = response.Data
            };
        }
    }
}
