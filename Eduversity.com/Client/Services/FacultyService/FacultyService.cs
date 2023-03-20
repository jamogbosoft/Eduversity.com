namespace Eduversity.com.Client.Services.FacultyService
{
    public class FacultyService : IFacultyService
    {
        private readonly HttpClient _http;

        public FacultyService(HttpClient http)
        {
            _http = http;
        }

        public event Action? FacultiesChanged;
        public List<FacultyReadDto> Faculties { get; set; } = new List<FacultyReadDto>();
        public List<Faculty> AdminFaculties { get; set; } = new List<Faculty>();
        public string Message { get; set; } = "Loading faculties...";

        public async Task<Faculty> CreateFaculty(Faculty faculty)
        {
            var result = await _http.PostAsJsonAsync("api/faculties", faculty);

            var newFaculty = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Faculty>>()).Data;
            return newFaculty;
        }

        public async Task DeleteFaculty(Faculty faculty)
        {
            var result = await _http.DeleteAsync($"api/faculties/{faculty.Id}");
        }

        public async Task GetAdminFaculties()
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<Faculty>>>("api/faculties/admin");
            if (result != null && result.Data != null)
            {
                AdminFaculties = result.Data;
            }

            if (AdminFaculties == null || AdminFaculties.Count == 0)
            {
                Message = "No faculty found.";
            }
        }

        public async Task<ServiceResponse<Faculty>> GetAdminFaculty(int facultyId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Faculty>>($"api/faculties/admin/{facultyId}");
            return result;
        }

        public async Task GetFaculties()
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<FacultyReadDto>>>("api/faculties");

            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No faculty found.";
                Faculties = new List<FacultyReadDto>();
                return;
            }
            Faculties = result.Data;
            FacultiesChanged?.Invoke();
        }

        public async Task<ServiceResponse<FacultyReadDto>> GetFaculty(int facultyId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<FacultyReadDto>>($"api/faculties/{facultyId}");
            return result;
        }

        public async Task<Faculty> UpdateFaculty(Faculty faculty)
        {
            var result = await _http.PutAsJsonAsync("api/faculties", faculty);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<Faculty>>();
            return content.Data;
        }
    }
}
