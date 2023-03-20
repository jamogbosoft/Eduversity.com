namespace Eduversity.com.Server.Services.StudentService
{
    public interface IStudentService
    {
        Task<ServiceResponse<List<StudentResponse>>> GetStudents();
        Task<ServiceResponse<List<StudentResponse>>> GetStudents(int optionId);
        Task<ServiceResponse<List<StudentResponse>>> GetStudents(int optionId, string currentSession, int currentLevel);
        Task<ServiceResponse<StudentResponse>> GetStudent(long userId = 0L);
        Task<ServiceResponse<StudentResponse>> AddOrUpdateStudent(StudentResponse studentResponse);
        Task<ServiceResponse<bool>> DeleteStudent(long studentId);
        Task<ServiceResponse<StudentSearchResponse>> SearchStudents(string searchText, int page, int optionId = 0, int pageSize = 15);
        Task<ServiceResponse<List<string>>> GetStudentSearchSuggestions(string searchText, int optionId = 0);
    }
}
