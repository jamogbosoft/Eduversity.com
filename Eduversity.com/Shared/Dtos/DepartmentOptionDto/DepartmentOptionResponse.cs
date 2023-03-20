using Eduversity.com.Shared.Models;

namespace Eduversity.com.Shared.Dtos.DepartmentOptionDto
{
    public class DepartmentOptionResponse: IDepartmentOptionResponse
    {
        public int FacultyId { get; set; }
        public int DepartmentId { get; set; }
        public string FacultyName { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public DepartmentOption Option { get; set; } = new DepartmentOption();
    }
}
