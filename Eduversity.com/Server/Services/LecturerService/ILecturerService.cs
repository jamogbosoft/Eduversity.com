namespace Eduversity.com.Server.Services.LecturerService
{
    public interface ILecturerService
    {
        Task<ServiceResponse<List<LecturerResponse>>> GetLecturers();
        Task<ServiceResponse<List<LecturerResponse>>> GetLecturers(int departmentId);
        Task<ServiceResponse<LecturerResponse>> GetLecturer(long userId = 0L);
        Task<ServiceResponse<LecturerResponse>> AddOrUpdateLecturer(LecturerResponse lecturerResponse);
        Task<ServiceResponse<bool>> DeleteLecturer(int lecturerId);
        Task<ServiceResponse<LecturerSearchResponse>> SearchLecturers(string searchText, int page, int departmentId = 0, int pageSize = 15);
        Task<ServiceResponse<List<string>>> GetLecturerSearchSuggestions(string searchText, int departmentId = 0);
    }
}
