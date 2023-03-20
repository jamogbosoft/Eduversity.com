namespace Eduversity.com.Client.Services.CountryService
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient _http;

        public CountryService(HttpClient http)
        {
            _http = http;
        }

        public List<CountryReadDto> Countries { get; set; } = new List<CountryReadDto>();
        public List<Country> AdminCountries { get; set; } = new List<Country>();
        public string Message { get; set; } = "Loading countries...";

        public async Task<Country> CreateCountry(Country country)
        {
            var result = await _http.PostAsJsonAsync("api/countries", country);

            var newCountry = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Country>>()).Data;
            return newCountry;

        }

        public async Task DeleteCountry(Country country)
        {
            var result = await _http.DeleteAsync($"api/countries/{country.Id}");
        }

        public async Task GetAdminCountries()
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<Country>>>("api/countries/admin");
            if (result != null && result.Data != null)
            {
                AdminCountries = result.Data;
            }

            if (AdminCountries == null || AdminCountries.Count == 0)
            {
                Message = "No country found.";
            }
        }
        public async Task GetCountries()
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<CountryReadDto>>>("api/countries");

            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No country found.";
                Countries = new List<CountryReadDto>();
                return;
            }
            Countries = result.Data;
        }
        public async Task<ServiceResponse<Country>> GetAdminCountry(int countryId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Country>>($"api/countries/admin/{countryId}");
            return result;
        }
        public async Task<ServiceResponse<CountryReadDto>> GetCountry(int countryId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<CountryReadDto>>($"api/countries/{countryId}");
            return result;
        }

        public async Task<Country> UpdateCountry(Country country)
        {
            var result = await _http.PutAsJsonAsync("api/countries", country);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<Country>>();
            if(content is null)
            {
                return null!;
            }
            var response = content.Data;
            return response;
        }
    }
}
