namespace Eduversity.com.Shared.Dtos.DepartmentOptionDto
{
    public interface IDepartmentOptionResponse
    {
        public int FacultyId { get; set; }
        public int DepartmentId { get; set; }
        public string FacultyName { get; set; }
        public string DepartmentName { get; set; }
    }
}
