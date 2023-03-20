using Eduversity.com.Shared.Dtos.LecturerDto;

namespace Eduversity.com.Client.Services.LecturerService
{
    public interface ILecturerService
    {
        event Action LecturersChanged;
        string Message { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        List<LecturerResponse> Lecturers { get; set; }
        Task GetLecturers();
        Task GetLecturers(int departmentId);
        Task<ServiceResponse<LecturerResponse>> GetLecturer();
        Task<ServiceResponse<LecturerResponse>> GetLecturer(long userId);
        Task<LecturerResponse> AddOrUpdateLecturer(LecturerResponse lecturer);
        Task DeleteLecturer(LecturerResponse lecturer);
        Task SearchLecturers(string searchText, int page);
        Task SearchLecturers(string searchText, int page, int departmentId);
        Task<List<string>> GetLecturerSearchSuggestions(string searchText);
        Task<List<string>> GetLecturerSearchSuggestions(string searchText, int departmentId);


    }
}
