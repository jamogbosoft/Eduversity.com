namespace Eduversity.com.Client.Services.LGAService
{
    public interface ILGAService
    {
        List<LGAReadDto> LGAs { get; set; }
        LGAsResponse AdminResponse { get; set; }
        string Message { get; set; }
        Task<ServiceResponse<LGAResponse>> GetAdminLGA(int lgaId);
        Task<ServiceResponse<LGAReadDto>> GetLGA(int lgaId);
        Task GetAdminLGAs(int stateId);
        Task GetLGAs(int stateId);
        Task<LGA> CreateLGA(LGA lga);
        Task<LGA> UpdateLGA(LGA lga);
        Task DeleteLGA(LGA lga);
    }
}
