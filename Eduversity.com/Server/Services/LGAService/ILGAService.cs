namespace Eduversity.com.Server.Services.LGAService
{
    public interface ILGAService
    {
        Task<ServiceResponse<LGAsResponse>> GetAdminLGAs(int stateId);
        Task<ServiceResponse<List<LGAReadDto>>> GetLGAs(int stateId);
        Task<ServiceResponse<LGAResponse>> GetAdminLGA(int lgaId);
        Task<ServiceResponse<LGAReadDto>> GetLGA(int lgaId);
        Task<ServiceResponse<LGA>> CreateLGA(LGA lga);
        Task<ServiceResponse<LGA>> UpdateLGA(LGA lga);
        Task<ServiceResponse<bool>> DeleteLGA(int lgaId);
    }
}
