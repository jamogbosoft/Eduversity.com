namespace Eduversity.com.Shared.Dtos.DepartmentDto
{
    public class DepartmentReadDto : ISchoolItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
    }
}
