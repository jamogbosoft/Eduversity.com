namespace Eduversity.com.Shared.Dtos.FacultyDto
{
    public class FacultyReadDto : ISchoolItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
    }
}
