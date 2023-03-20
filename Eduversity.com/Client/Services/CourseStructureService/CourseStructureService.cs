namespace Eduversity.com.Client.Services.CourseStructureService
{
    public class CourseStructureService : ICourseStructureService
    {
        private readonly HttpClient _http;

        public CourseStructureService(HttpClient http)
        {
            _http = http;
        }

        public event Action? ListOfCoursesChanged;
        public string Message { get; set; } = "Loading courses...";        

        public List<CourseStructureResponse> ListOfCourses { get; set; } = new List<CourseStructureResponse>();
                
        public async Task<CourseStructureResponse> AddCourse(CourseStructureRequest courseStructureRequest)
        {
            var response = await _http.PostAsJsonAsync("api/coursestructures", courseStructureRequest);
            if (!response.IsSuccessStatusCode)
            {
                Message = "Unable to add a course.";
                return null!;
            }
            var result = await response.Content
                .ReadFromJsonAsync<ServiceResponse<CourseStructureResponse>>();
            if (result == null)
            {
                Message = "Unable to add a course.";
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

        public async Task GetListOfCourses(int optionId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<CourseStructureResponse>>>($"api/coursestructures/option/{optionId}");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No course found.";
                ListOfCourses = new List<CourseStructureResponse>();
            }
            else
            {
                ListOfCourses = result.Data;
            }

            ListOfCoursesChanged?.Invoke();
        }

        public async Task GetListOfCourses(int optionId, int level, string semester)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<CourseStructureResponse>>>($"api/coursestructures/option/{optionId}/{level}/{semester}");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No course found.";
                ListOfCourses = new List<CourseStructureResponse>();
            }
            else
            {
                ListOfCourses = result.Data;
            }

            ListOfCoursesChanged?.Invoke();
        }

        public async Task RemoveCourse(long structureId)
        {
            var response = await _http.DeleteAsync($"api/coursestructures/{structureId}");
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
    }
}
