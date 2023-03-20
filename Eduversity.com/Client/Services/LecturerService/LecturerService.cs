
namespace Eduversity.com.Client.Services.LecturerService
{
    public class LecturerService : ILecturerService
    {
        private readonly HttpClient _http;

        public LecturerService(HttpClient  http)
        {
            _http = http;
        }
        public string Message { get; set; } = "Loading Lecturers...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;
        public List<LecturerResponse> Lecturers { get; set; } = new();

        public event Action LecturersChanged;

        public async Task<LecturerResponse> AddOrUpdateLecturer(LecturerResponse lecturer)
        {
            var response = await _http.PostAsJsonAsync("api/lecturers", lecturer);
            var result = await response.Content
                .ReadFromJsonAsync<ServiceResponse<LecturerResponse>>();
            if (result == null)
            {
                Message = "Action was not successful.";
                return null!;
            }
            return result.Data!;
        }

        public async Task DeleteLecturer(LecturerResponse lecturer)
        {
            var result = await _http.DeleteAsync($"api/lecturers/{lecturer.Id}");
        }

        public async Task<ServiceResponse<LecturerResponse>> GetLecturer(long userId)
        {
            var result = await _http
              .GetFromJsonAsync<ServiceResponse<LecturerResponse>>($"api/lecturers/admin/{userId}");
            return result!;
        }

        public async Task<ServiceResponse<LecturerResponse>> GetLecturer()
        {
            var result = await _http
               .GetFromJsonAsync<ServiceResponse<LecturerResponse>>("api/lecturers/lecturer");
            return result!;
        }

        public async Task GetLecturers()
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<LecturerResponse>>>("api/lecturers/admin");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No lecturer found.";
                Lecturers = new List<LecturerResponse>();
            }
            else
            {
                Lecturers = result.Data;
            }

            CurrentPage = 1;
            PageCount = 0;

            LecturersChanged?.Invoke();
        }

        public async Task GetLecturers(int departmentId)
        {
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<List<LecturerResponse>>>($"api/lecturers/admin/department/{departmentId}");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No lecturer found.";
                Lecturers = new List<LecturerResponse>();
            }
            else
            {
                Lecturers = result.Data;
            }

            CurrentPage = 1;
            PageCount = 0;

            LecturersChanged?.Invoke();
        }

        public async Task<List<string>> GetLecturerSearchSuggestions(string searchText)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/lecturers/admin/searchsuggestions/{searchText}");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                return new List<string>();
            }
            return result.Data;
        }

        public async Task<List<string>> GetLecturerSearchSuggestions(string searchText, int departmentId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/lecturers/admin/department/{departmentId}/searchsuggestions/{searchText}");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                return new List<string>();
            }
            return result.Data;
        }

        public async Task SearchLecturers(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<LecturerSearchResponse>>($"api/lecturers/admin/search/{searchText}/{page}");

            if (result == null || result.Data == null)
            {
                Message = "No lecturer found.";
                Lecturers = new List<LecturerResponse>();
                CurrentPage = 1;
                PageCount = 0;
            }
            else
            {
                Lecturers = result.Data.Lecturers;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            LecturersChanged?.Invoke();
        }

        public async Task SearchLecturers(string searchText, int page, int departmentId)
        {
            LastSearchText = searchText;
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<LecturerSearchResponse>>($"api/lecturers/admin/department/{departmentId}/search/{searchText}/{page}");

            if (result == null || result.Data == null)
            {
                Message = "No lecturer found.";
                Lecturers = new List<LecturerResponse>();
                CurrentPage = 1;
                PageCount = 0;
            }
            else
            {
                Lecturers = result.Data.Lecturers;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            LecturersChanged?.Invoke();
        }
    }
}
