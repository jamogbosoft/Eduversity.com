namespace Eduversity.com.Client.Services.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly HttpClient _http;

        public DepartmentService(HttpClient http)
        {
            _http = http;
        }

        public event Action? DepartmentsChanged;
        public List<DepartmentReadDto> Departments { get; set; } = new List<DepartmentReadDto>();
        public DepartmentsResponse AdminResponse { get; set; } = new DepartmentsResponse();
        public string Message { get; set; } = string.Empty;

        public async Task<Department> CreateDepartment(Department department)
        {
            var result = await _http.PostAsJsonAsync("api/departments", department);
            var newDepartment = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Department>>())!.Data;
            return newDepartment!;
        }

        public async Task DeleteDepartment(Department department)
        {
            var result = await _http.DeleteAsync($"api/departments/{department.Id}");
        }

        public async Task<ServiceResponse<DepartmentResponse>> GetAdminDepartment(int departmentId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<DepartmentResponse>>($"api/departments/admin-dId/{departmentId}");
            return result!;
        }

        public async Task GetAdminDepartments(int facultyId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<DepartmentsResponse>>($"api/departments/admin-fId/{facultyId}");

            if (result != null)
            {
                if (result.Success)
                {
                    if (result.Data != null)
                    {
                        AdminResponse = result.Data;
                    }
                    if (AdminResponse.Departments == null || AdminResponse.Departments.Count == 0)
                    {
                        Message = $"There is no department in '{AdminResponse.FacultyName}'!";
                    }
                }
                else { Message = result.Message; }
            }
            else { Message = "Nothing was returned."; }
        }

        public async Task<ServiceResponse<DepartmentReadDto>> GetDepartment(int departmentId)
        {
            var result = await _http
               .GetFromJsonAsync<ServiceResponse<DepartmentReadDto>>($"api/departments/user-dId/{departmentId}");
            return result!;
        }

        public async Task GetDepartments(int facultyId)
        {
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<List<DepartmentReadDto>>>($"api/departments/user-fId/{facultyId}");

            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No department found.";
                Departments = new List<DepartmentReadDto>();
                return;
            }
            Departments = result.Data;
            DepartmentsChanged?.Invoke();
        }

        public async Task<Department> UpdateDepartment(Department department)
        {
            var result = await _http.PutAsJsonAsync($"api/departments", department);
            var updatedDepartment = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Department>>())!.Data;
            return updatedDepartment!;
        }
    }
}
