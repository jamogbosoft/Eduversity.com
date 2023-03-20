namespace Eduversity.com.Client.Services.CountryService
{
    public interface ICountryService
    {
        List<CountryReadDto> Countries { get; set; }
        List<Country> AdminCountries { get; set; }
        string Message { get; set; }
        Task<ServiceResponse<Country>> GetAdminCountry(int countryId);
        Task<ServiceResponse<CountryReadDto>> GetCountry(int countryId);
        Task GetAdminCountries();
        Task GetCountries();
        Task<Country> CreateCountry(Country country);
        Task<Country> UpdateCountry(Country country);
        Task DeleteCountry(Country country);
    }
}
