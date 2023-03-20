namespace Eduversity.com.Server.Services.LGAService
{
    public class LGAService : ILGAService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LGAService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<LGA>> CreateLGA(LGA lga)
        {
            _context.LGAs.Add(lga);
            await _context.SaveChangesAsync();
            return new ServiceResponse<LGA> { Data = lga };
        }

        public async Task<ServiceResponse<bool>> DeleteLGA(int lgaId)
        {
            var dbLGA = await _context.LGAs.FindAsync(lgaId);
            if (dbLGA == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "LGA not found."
                };
            }

            dbLGA.IsDeleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<LGAsResponse>> GetAdminLGAs(int stateId)
        {
            var lgas = await _context.LGAs
                     .Where(l => !l.IsDeleted && l.StateId == stateId)
                     .OrderBy(l => l.Name)
                     .ToListAsync();

            var item = await (from s in _context.States
                              join c in _context.Countries
                              on s.CountryId equals c.Id
                              where s.Id == stateId
                              select new
                              {
                                  CountryId = c.Id,
                                  CountryName = c.Name,
                                  StateId = s.Id,
                                  StateName = s.Name
                              }).FirstOrDefaultAsync();

            if (item == null)
            {
                return new ServiceResponse<LGAsResponse>
                {
                    Data = null,
                    Success = false,
                    Message = $"Sorry! There is no state or country with this state Id '{stateId}'"

                };
            }

            var response = new ServiceResponse<LGAsResponse>
            {
                Data = new LGAsResponse()
                {
                    CountryId = item.CountryId,
                    CountryName = item.CountryName,
                    StateId = item.StateId,
                    StateName = item.StateName,
                    LGAs = lgas
                }
            };

            return response;
        }

        public async Task<ServiceResponse<LGAResponse>> GetAdminLGA(int lgaId)
        {
            var response = new ServiceResponse<LGAResponse>();
            LGA lga;

            var result = await _context.LGAs
                          .FirstOrDefaultAsync(l => l.Id == lgaId && !l.IsDeleted);
            lga = result!;

            if (lga == null)
            {
                response.Success = false;
                response.Message = $"Sorry! There is no LGA with this id '{lgaId}'";
            }
            else
            {
                var item = await (from s in _context.States
                                  join c in _context.Countries
                                  on s.CountryId equals c.Id
                                  where s.Id == lga.StateId
                                  select new
                                  {
                                      CountryId = c.Id,
                                      CountryName = c.Name,
                                      StateId = s.Id,
                                      StateName = s.Name
                                  }).FirstOrDefaultAsync();

                if (item == null)
                {
                    response.Success = false;
                    response.Message = $"Sorry! There is no state or country for this LGA '{lga.Name}' with LGA Id '{lga.Id}'";
                }
                else
                {
                    response.Data = new LGAResponse()
                    {
                        CountryId = item.CountryId,
                        CountryName = item.CountryName,
                        StateId = item.StateId,
                        StateName = item.StateName,
                        LGA = lga
                    };
                }
            }

            return response;
        }

        public async Task<ServiceResponse<LGAReadDto>> GetLGA(int lgaId)
        {
            LGAReadDto? lga = null;

            var result = await _context.LGAs
                .FirstOrDefaultAsync(l => l.Id == lgaId && l.IsActive && !l.IsDeleted);

            if (result == null)
            {
                return new ServiceResponse<LGAReadDto>()
                {
                    Success = false,
                    Message = $"Sorry! There is no LGA with this id '{lgaId}'",
                    Data = null
                };
            }
            lga = _mapper.Map<LGAReadDto>(result);
            return new ServiceResponse<LGAReadDto>()
            {
                Data = lga
            };
        }

        public async Task<ServiceResponse<List<LGAReadDto>>> GetLGAs(int stateId)
        {
            List<LGAReadDto>? lgas = null;

            var result = await _context.LGAs
                        .Where(l => l.IsActive && !l.IsDeleted && l.StateId == stateId)
                        .OrderBy(l => l.Name)
                        .ToListAsync();

            if (result == null || result.Count == 0)
            {
                return new ServiceResponse<List<LGAReadDto>>
                {
                    Success = false,
                    Message = $"Sorry! There is no state or country with this state id '{stateId}'",
                    Data = null
                };
            }

            lgas = _mapper.Map<List<LGAReadDto>>(result);
            return new ServiceResponse<List<LGAReadDto>>
            {
                Data = lgas
            };
        }
        public async Task<ServiceResponse<LGA>> UpdateLGA(LGA lga)
        {
            var dbLGA = await _context.LGAs.FindAsync(lga.Id);
            if (dbLGA == null)
            {
                return new ServiceResponse<LGA>
                {
                    Success = false,
                    Message = "LGA not found."
                };
            }

            dbLGA.Name = lga.Name;
            dbLGA.IsActive = lga.IsActive;
            dbLGA.IsDeleted = lga.IsDeleted;

            await _context.SaveChangesAsync();
            return new ServiceResponse<LGA> { Data = lga };
        }
    }
}