namespace Eduversity.com.Client.Services.DepartmentOptionService
{
    public interface IDepartmentOptionService
    {
        event Action DepartmentOptionsChanged;
        List<DepartmentOptionReadDto> DepartmentOptions { get; set; }
        DepartmentOptionsResponse AdminResponse { get; set; }
        string Message { get; set; }
        Task<ServiceResponse<DepartmentOptionResponse>> GetAdminDepartmentOption(int optionId);
        Task<ServiceResponse<DepartmentOptionReadDto>> GetDepartmentOption(int optionId);
        Task GetAdminDepartmentOptions(int departmentId);
        Task GetDepartmentOptions();
        Task GetDepartmentOptions(int departmentId);
        Task<DepartmentOption> CreateDepartmentOption(DepartmentOption option);
        Task<DepartmentOption> UpdateDepartmentOption(DepartmentOption option);
        Task DeleteDepartmentOption(DepartmentOption option);
    }
}
