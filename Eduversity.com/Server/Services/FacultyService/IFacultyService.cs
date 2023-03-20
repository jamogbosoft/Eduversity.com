namespace Eduversity.com.Server.Services.FacultyService
{
    public interface IFacultyService
    {
        Task<ServiceResponse<List<Faculty>>> GetAdminFaculties();
        Task<ServiceResponse<List<FacultyReadDto>>> GetFaculties();
        Task<ServiceResponse<Faculty>> GetAdminFaculty(int facultyId);
        Task<ServiceResponse<FacultyReadDto>> GetFaculty(int facultyId);
        Task<ServiceResponse<Faculty>> CreateFaculty(Faculty faculty);
        Task<ServiceResponse<Faculty>> UpdateFaculty(Faculty faculty);
        Task<ServiceResponse<bool>> DeleteFaculty(int facultyId);
    }
}
