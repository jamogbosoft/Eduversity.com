using Eduversity.com.Shared.Dtos.DepartmentHeadDto;

namespace Eduversity.com.Client.Services.DepartmentHeadService
{
    public interface IDepartmentHeadService
    {
        event Action DepartmentHeadsChanged;
        event Action DepartmentHeadChanged;
        string Message { get; set; }
        int CurrentOptionId { get; set; }
        List<DepartmentHeadResponse> DepartmentHeads { get; set; }
        DepartmentHeadResponse DepartmentHead { get; set; }

        Task GetDepartmentHeads();
        Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHead();
        Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByUserId(long userId);
        Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByDepartmentId(int departmentId);
        Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByHeadId(int headId);
        Task<DepartmentHeadResponse> AddDepartmentHead(DepartmentHeadRequest request);
        Task<DepartmentHeadResponse> UpdateDepartmentHead(int headId, DepartmentHeadRequest request);
    }
}
