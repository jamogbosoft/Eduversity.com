using Eduversity.com.Shared.Models;

namespace Eduversity.com.Shared.Dtos.DepartmentDto
{
    public  class DepartmentResponse: IDepartmentResponse
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = string.Empty;
        public Department Department { get; set; } = new Department();
    }
}
