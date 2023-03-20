namespace Eduversity.com.Client.Services.DepartmentService
{
    public interface IDepartmentService
    {
        event Action DepartmentsChanged;
        List<DepartmentReadDto> Departments { get; set; }
        DepartmentsResponse AdminResponse { get; set; }
        string Message { get; set; }
        Task<ServiceResponse<DepartmentResponse>> GetAdminDepartment(int departmentId);
        Task<ServiceResponse<DepartmentReadDto>> GetDepartment(int departmentId);
        Task GetAdminDepartments(int facultyId);
        Task GetDepartments(int facultyId);
        Task<Department> CreateDepartment(Department department);
        Task<Department> UpdateDepartment(Department department);
        Task DeleteDepartment(Department department);
    }
}
