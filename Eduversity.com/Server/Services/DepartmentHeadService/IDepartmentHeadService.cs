using Eduversity.com.Shared.Dtos.DepartmentHeadDto;

namespace Eduversity.com.Server.Services.DepartmentHeadService
{
    public interface IDepartmentHeadService
    {
        Task<ServiceResponse<List<DepartmentHeadResponse>>> GetListOfDepartmentHeads();
        Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHead();
        Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByUserId(long userId);
        Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByDepartmentId(int departmentId);
        Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByHeadId(int headId);
        Task<ServiceResponse<DepartmentHeadResponse>> AddDepartmentHead(DepartmentHeadRequest request);
        Task<ServiceResponse<DepartmentHeadResponse>> UpdateDepartmentHead(int headId, DepartmentHeadRequest request);
    }
}
