
namespace Eduversity.com.Server.Services.DepartmentService
{
    public interface IDepartmentService
    {
        Task<ServiceResponse<DepartmentsResponse>> GetAdminDepartments(int facultyId);
        Task<ServiceResponse<List<DepartmentReadDto>>> GetDepartments(int facultyId);
        Task<ServiceResponse<DepartmentResponse>> GetAdminDepartment(int departmentId);
        Task<ServiceResponse<DepartmentReadDto>> GetDepartment(int departmentId);
        Task<ServiceResponse<Department>> CreateDepartment(Department department);
        Task<ServiceResponse<Department>> UpdateDepartment(Department department);
        Task<ServiceResponse<bool>> DeleteDepartment(int departmentId);
    }
}
