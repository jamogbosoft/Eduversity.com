using Microsoft.CodeAnalysis;

namespace Eduversity.com.Server.Services.CountryService
{
    public class CountryService : ICountryService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CountryService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<Country>>> GetAdminCountries()
        {
            var response = new ServiceResponse<List<Country>>
            {
                Data = await _context.Countries
                     .Where(c => !c.IsDeleted).OrderBy(c => c.Name)
                     .Include(c => c.States.Where(s => !s.IsDeleted).OrderBy(s => s.Name))
                     .ThenInclude(s => s.LGAs.Where(l => !l.IsDeleted).OrderBy(l => l.Name))
                     .ToListAsync()
            };

            return response;
        }

        public async Task<ServiceResponse<List<CountryReadDto>>> GetCountries()
        {
            List<CountryReadDto>? countries = null;
            var result = await _context.Countries
                    .Where(c => c.IsActive && !c.IsDeleted).OrderBy(c => c.Name)
                   // .Include(c => c.States.Where(s => s.IsActive &&  !s.IsDeleted).OrderBy(s => s.Name))   //
                  //  .ThenInclude(s => s.LGAs.Where(l => l.IsActive && !l.IsDeleted).OrderBy(l => l.Name)) //
                    .ToListAsync();

            countries = _mapper.Map<List<CountryReadDto>>(result);

            return new ServiceResponse<List<CountryReadDto>>
            {
                Data = countries
            };
        }

        public async Task<ServiceResponse<Country>> GetAdminCountry(int countryId)
        {
            var response = new ServiceResponse<Country>();
            Country? country = null;

            var result = await _context.Countries
                        .Include(c => c.States.Where(s => !s.IsDeleted).OrderBy(s => s.Name))
                        .ThenInclude(s => s.LGAs.Where(l => !l.IsDeleted).OrderBy(l => l.Name))
                        .FirstOrDefaultAsync(c => c.Id == countryId && !c.IsDeleted);
            country = result;

            if (country == null)
            {
                response.Success = false;
                response.Message = "Sorry! This country does not exist.";
            }
            else
            {
                response.Data = country;
            }

            return response;
        }

        public async Task<ServiceResponse<CountryReadDto>> GetCountry(int countryId)
        {
            CountryReadDto? country = null;

            var result = await _context.Countries
                 //.Include(c => c.States.Where(s => s.IsActive && !s.IsDeleted).OrderBy(s => s.Name))
                 //.ThenInclude(s => s.LGAs.Where(l => l.IsActive && !l.IsDeleted).OrderBy(l => l.Name))
                 .FirstOrDefaultAsync(c => c.Id == countryId && c.IsActive && !c.IsDeleted);
                        
            if (result == null)
            {
                return new ServiceResponse<CountryReadDto>()
                {
                    Success = false,
                    Message = "Sorry! This country does not exist.",
                    Data = null
                };
            }

            country = _mapper.Map<CountryReadDto>(result);

            return new ServiceResponse<CountryReadDto>()
            {
                Success = false,
                Message = "Sorry! This country does not exist.",
                Data = country
            };
        }

        public async Task<ServiceResponse<Country>> CreateCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Country> { Data = country };
        }

        public async Task<ServiceResponse<Country>> UpdateCountry(Country country)
        {
            var dbCountry = await _context.Countries.FindAsync(country.Id);
            if (dbCountry == null)
            {
                return new ServiceResponse<Country>
                {                    
                    Success = false,
                    Message = "Country not found."
                };
            }

            dbCountry.Name = country.Name;
            dbCountry.IsActive = country.IsActive;
            dbCountry.IsDeleted = country.IsDeleted;

            foreach (var state in country.States)
            {
                var dbState = await _context.States.FindAsync(state.Id);
                if (dbState == null)
                {
                    //Add new state
                    _context.States.Add(state);
                }
                else
                {
                    //Update a particular state
                    dbState.CountryId = state.CountryId;
                    dbState.Name = state.Name;
                    dbState.IsActive = state.IsActive;
                    dbState.IsDeleted = state.IsDeleted;
                }
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<Country> { Data = country };
        }

        public async Task<ServiceResponse<bool>> DeleteCountry(int countryId)
        {
            var dbCountry = await _context.Countries.FindAsync(countryId);
            if (dbCountry == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Country not found."
                };
            }

            dbCountry.IsDeleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }
    }
}
