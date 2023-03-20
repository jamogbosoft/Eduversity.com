
namespace Eduversity.com.Client.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient _http;

        public CourseService(HttpClient http)
        {
            _http = http;            
        }

        public string Message { get; set; } = "Loading courses...";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } = string.Empty;

        public List<CourseResponse> Courses { get; set ; } = new List<CourseResponse>();

        public event Action CoursesChanged;

        public async Task<CourseResponse> CreateCourse(CourseRequest courseRequest)
        {
            var response = await _http.PostAsJsonAsync("api/courses", courseRequest);
            var result = await response.Content
                .ReadFromJsonAsync<ServiceResponse<CourseResponse>>();
            if (result == null)
            {
                Message = "Unable to create a new course.";
                return null!;
            }
            else if (result.Data == null)
            {
                Message = result.Message;
                return null!;
            }
            else
            {
                return result.Data;
            }
        }
        public async Task<CourseResponse> UpdateCourse(int courseId, CourseRequest courseRequest)
        {
            var response = await _http.PutAsJsonAsync($"api/courses/{courseId}", courseRequest);
            var result = await response.Content
                .ReadFromJsonAsync<ServiceResponse<CourseResponse>>();
            if (result == null)
            {
                Message = "Unable to update a course.";
                return null!;
            }
            else if (result.Data == null)
            {
                Message = result.Message;
                return null!;
            }
            else
            {
                return result.Data;
            }
        }

        public async Task DeleteCourse(int courseId)
        {
            var response = await _http.DeleteAsync($"api/courses/{courseId}");
            var result = await response.Content
                .ReadFromJsonAsync<ServiceResponse<bool>>();
            if (result == null)
            {
                Message = "Delete action was not successful.";
            }            
            else
            {
                Message = result.Message;
            }
        }

        public async Task<CourseResponse> GetCourseById(int courseId)
        {
            var result = await _http
               .GetFromJsonAsync<ServiceResponse<CourseResponse>>($"api/courses/{courseId}");
            if (result == null)
            {
                Message = "Search action was not successful.";
                return null!;
            }
            else if (result.Data == null)
            {
                Message = result.Message;
                return null!;
            }
            else
            {
                return result.Data;
            }
        }

        public async Task GetCourses()
        {
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<List<CourseResponse>>>("api/courses");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No course found.";
                Courses = new List<CourseResponse>();
            }
            else
            {
                Courses = result.Data;
            }

            CurrentPage = 1;
            PageCount = 0;

            CoursesChanged?.Invoke();
        }

        public async Task GetDeletedCourses()
        {
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<List<CourseResponse>>>("api/courses/deleted");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No course found.";
                Courses = new List<CourseResponse>();
            }
            else
            {
                Courses = result.Data;
            }

            CurrentPage = 1;
            PageCount = 0;
            CoursesChanged?.Invoke();
        }

        public async Task<List<string>> GetCourseSearchSuggestions(string searchText)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/courses/searchsuggestions/{searchText}");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                return new List<string>();
            }
            return result.Data;
        }

        public async Task SearchCourses(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<CourseSearchResponse>>($"api/courses/search/{searchText}/{page}");

            if (result == null || result.Data == null)
            {
                Message = "No course found.";
                Courses = new List<CourseResponse>();
                CurrentPage = 1;
                PageCount = 0;
            }
            else
            {
                Courses = result.Data.Courses;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }

            CoursesChanged?.Invoke();
        }
    }
}
