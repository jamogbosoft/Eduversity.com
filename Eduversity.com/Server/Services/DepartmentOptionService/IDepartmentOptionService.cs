namespace Eduversity.com.Server.Services.DepartmentOptionService
{
    public interface IDepartmentOptionService
    {
        Task<ServiceResponse<DepartmentOptionsResponse>> GetAdminDepartmentOptions(int departmentId);
        Task<ServiceResponse<List<DepartmentOptionReadDto>>> GetDepartmentOptions();
        Task<ServiceResponse<List<DepartmentOptionReadDto>>> GetDepartmentOptions(int departmentId);
        Task<ServiceResponse<DepartmentOptionResponse>> GetAdminDepartmentOption(int optionId);
        Task<ServiceResponse<DepartmentOptionReadDto>> GetDepartmentOption(int optionId);
        Task<ServiceResponse<DepartmentOption>> CreateDepartmentOption(DepartmentOption option);
        Task<ServiceResponse<DepartmentOption>> UpdateDepartmentOption(DepartmentOption option);
        Task<ServiceResponse<bool>> DeleteDepartmentOption(int optionId);
    }
}
