namespace Eduversity.com.Shared.Dtos.StudentDto
{
    public class StudentSearchResponse
    {
        public List<StudentResponse> Students { get; set; } = new();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
