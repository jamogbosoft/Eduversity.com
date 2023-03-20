using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eduversity.com.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController (ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet("admin"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<List<Country>>>> GetAdminCountries()
        {
            var result = await _countryService.GetAdminCountries();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CountryReadDto>>>> GetCountries()
        {
            var result = await _countryService.GetCountries();
            return Ok(result);
        }

        [HttpGet("admin/{countryId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Country>>> GetAdminCountry(int countryId)
        {
            var result = await _countryService.GetAdminCountry(countryId);
            return Ok(result);
        }

        [HttpGet("{countryId}")]
        public async Task<ActionResult<ServiceResponse<CountryReadDto>>> GetCountry(int countryId)
        {
            var result = await _countryService.GetCountry(countryId);
            return Ok(result);
        }

        [HttpPost, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Country>>> CreateCountry(Country country)
        {
            var result = await _countryService.CreateCountry(country);
            return Ok(result);
        }

        [HttpPut, Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<Country>>> UpdateCountry(Country country)
        {
            var result = await _countryService.UpdateCountry(country);
            return Ok(result);
        }

        [HttpDelete("{countryId}"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteCountry(int countryId)
        {
            var result = await _countryService.DeleteCountry(countryId);
            return Ok(result);
        }

    }
}
