
namespace Eduversity.com.Client.Services.StudentService
{
    public interface IStudentService
    {
        event Action StudentsChanged;
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }        
        List<StudentResponse> Students { get; set; }    
        Task GetStudents();
        Task GetStudents(int optionId);
        Task GetStudents(int optionId, string session, int level);
        Task<ServiceResponse<StudentResponse>> GetStudent();
        Task<ServiceResponse<StudentResponse>> GetStudent(long userId);
        Task SearchStudents(string searchText, int page);
        Task SearchStudents(string searchText, int page, int optionId);
        Task<List<string>> GetStudentSearchSuggestions(string searchText);
        Task<List<string>> GetStudentSearchSuggestions(string searchText, int optionId);
        Task<StudentResponse> AddOrUpdateStudent(StudentResponse student);
        Task DeleteStudent(StudentResponse student);
    }
}
