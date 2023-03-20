namespace Eduversity.com.Shared.Dtos.LecturerDto
{
    public class LecturerSearchResponse
    {
        public List<LecturerResponse> Lecturers { get; set; } = new();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
