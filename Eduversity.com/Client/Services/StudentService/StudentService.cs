using Microsoft.AspNetCore.WebUtilities;

namespace Eduversity.com.Client.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _http;

        public StudentService(HttpClient http)
        {
            _http = http;
        }

       
        public string Message { get; set; } = "Loading students...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;

        public event Action? StudentsChanged;
        public List<StudentResponse> Students { get; set; } = new();
        public async Task<StudentResponse> AddOrUpdateStudent(StudentResponse student)
        {
            var response = await _http.PostAsJsonAsync("api/students", student);
            var result = await response.Content
                .ReadFromJsonAsync<ServiceResponse<StudentResponse>>();
            if (result == null)
            {
                Message = "Action was not successful.";
                return null!;
            }
            return result.Data!;
        }

        public async Task DeleteStudent(StudentResponse student)
        {
            var result = await _http.DeleteAsync($"api/students/{student.Id}");
        }

        public async Task<ServiceResponse<StudentResponse>> GetStudent()
        {
            var result = await _http
               .GetFromJsonAsync<ServiceResponse<StudentResponse>>("api/students/student");
            return result!;
        }
        public async Task<ServiceResponse<StudentResponse>> GetStudent(long userId)
        {
            var result = await _http
              .GetFromJsonAsync<ServiceResponse<StudentResponse>>($"api/students/admin/{userId}");
            return result!;
        }

        public async Task GetStudents()
        {
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<List<StudentResponse>>>("api/students/admin");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No student found.";
                Students = new List<StudentResponse>();
            }
            else
            {
                Students = result.Data;
            }

            CurrentPage = 1;
            PageCount = 0;

            StudentsChanged?.Invoke();
        }

        public async Task GetStudents(int optionId)
        {
            var result = await _http
                  .GetFromJsonAsync<ServiceResponse<List<StudentResponse>>>($"api/students/admin/option/{optionId}");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No student found.";
                Students = new List<StudentResponse>();
            }
            else
            {
                Students = result.Data;
            }

            CurrentPage = 1;
            PageCount = 0;

            StudentsChanged?.Invoke();
        }

        public async Task GetStudents(int optionId, string session, int level)
        {
            var query = new Dictionary<string, string> {
                { "optionId", optionId.ToString() },
                { "currentSession", session.ToString() },
                { "currentLevel", level.ToString() }
            };

            var queryParameter = QueryHelpers.AddQueryString("", query);

            var result = await _http
                  .GetFromJsonAsync<ServiceResponse<List<StudentResponse>>>($"api/students/admin/program{queryParameter}");

            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No student found.";
                Students = new List<StudentResponse>();
            }
            else
            {
                Students = result.Data;
            }

            CurrentPage = 1;
            PageCount = 0;

            StudentsChanged?.Invoke();
        }

        public async Task<List<string>> GetStudentSearchSuggestions(string searchText)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/students/admin/searchsuggestions/{searchText}");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                return new List<string>();
            }
            return result.Data;
        }

        public async Task<List<string>> GetStudentSearchSuggestions(string searchText, int optionId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/students/admin/option/{optionId}/searchsuggestions/{searchText}");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                return new List<string>();
            }
            return result.Data;
        }

        public async Task SearchStudents(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<StudentSearchResponse>>($"api/students/admin/search/{searchText}/{page}");

            if (result == null || result.Data == null)
            {
                Message = "No student found.";
                Students = new List<StudentResponse>();
                CurrentPage = 1;
                PageCount = 0;
            }
            else
            {
                Students = result.Data.Students;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            StudentsChanged?.Invoke();
        }

        public async Task SearchStudents(string searchText, int page, int optionId)
        {
            LastSearchText = searchText;
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<StudentSearchResponse>>($"api/students/admin/option/{optionId}/search/{searchText}/{page}");

            if (result == null || result.Data == null)
            {
                Message = "No student found.";
                Students = new List<StudentResponse>();
                CurrentPage = 1;
                PageCount = 0;
            }
            else
            {
                Students = result.Data.Students;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            StudentsChanged?.Invoke();
        }
    }
}
