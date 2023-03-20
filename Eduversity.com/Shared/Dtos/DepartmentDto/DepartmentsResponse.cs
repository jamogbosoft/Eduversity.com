using Eduversity.com.Shared.Models;

namespace Eduversity.com.Shared.Dtos.DepartmentDto
{
    public  class DepartmentsResponse: IDepartmentResponse
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = string.Empty;
        public List<Department> Departments { get; set; } = new List<Department>();
    }
}
