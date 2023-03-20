namespace Eduversity.com.Server.Services.CountryService
{
    public interface ICountryService
    {
        Task<ServiceResponse<List<Country>>> GetAdminCountries();
        Task<ServiceResponse<List<CountryReadDto>>> GetCountries();
        Task<ServiceResponse<Country>> GetAdminCountry(int countryId);
        Task<ServiceResponse<CountryReadDto>> GetCountry(int countryId);        
        Task<ServiceResponse<Country>> CreateCountry(Country country);
        Task<ServiceResponse<Country>> UpdateCountry(Country country);
        Task<ServiceResponse<bool>> DeleteCountry(int countryId);
    }
}
