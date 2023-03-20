namespace Eduversity.com.Client.Services.FacultyService
{
    public interface IFacultyService
    {
        event Action FacultiesChanged;
        List<FacultyReadDto> Faculties { get; set; }
        List<Faculty> AdminFaculties { get; set; }
        string Message { get; set; }
        Task<ServiceResponse<Faculty>> GetAdminFaculty(int facultyId);
        Task<ServiceResponse<FacultyReadDto>> GetFaculty(int facultyId);
        Task GetAdminFaculties();
        Task GetFaculties();
        Task<Faculty> CreateFaculty(Faculty faculty);
        Task<Faculty> UpdateFaculty(Faculty faculty);
        Task DeleteFaculty(Faculty faculty);
    }
}
