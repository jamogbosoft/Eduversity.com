using Microsoft.CodeAnalysis;

namespace Eduversity.com.Server.Services.StateService
{
    public class StateService : IStateService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public StateService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<StatesResponse>> GetAdminStates(int countryId)
        {
            var states = await _context.States
                     .Where(s => !s.IsDeleted && s.CountryId == countryId).OrderBy(s => s.Name)
                     .Include(s => s.LGAs.Where(l => !l.IsDeleted).OrderBy(l => l.Name))
                     .ToListAsync();
            var country = await _context.Countries.FindAsync(countryId);

            if (country == null)
            {
                return new ServiceResponse<StatesResponse>
                {
                    Data = null,
                    Success = false,
                    Message = $"There is no country with this id '{countryId}'"                    
                };
            }

            var response = new ServiceResponse<StatesResponse>
            {
                Data = new StatesResponse()
                {
                    CountryId = country.Id,
                    CountryName = country.Name,
                    States = states
                }               
            };            

            return response;
        }

        public async Task<ServiceResponse<List<StateReadDto>>> GetStates(int countryId)
        {
            List<StateReadDto>? states = null;

            var result = await _context.States
                        .Where(s => s.IsActive && !s.IsDeleted && s.CountryId == countryId).OrderBy(s => s.Name)
                        //.Include(s => s.LGAs.Where(l => l.IsActive && !l.IsDeleted).OrderBy(l => l.Name))
                        .ToListAsync();

            if (result == null || result.Count == 0)
            {
                return new ServiceResponse<List<StateReadDto>>()
                {
                    Success = false,
                    Message = $"Sorry! There is no state with this country id '{countryId}'",
                    Data = null
                };
            }

            states = _mapper.Map<List<StateReadDto>>(result);
            return new ServiceResponse<List<StateReadDto>>
            {
                Data = states
            };
        }

        public async Task<ServiceResponse<StateResponse>> GetAdminState(int stateId)
        {
            var response = new ServiceResponse<StateResponse>();
            State state;
                        
                var result = await _context.States
                            .Include(s => s.LGAs.Where(l => !l.IsDeleted).OrderBy(l => l.Name))
                            .FirstOrDefaultAsync(s => s.Id == stateId && !s.IsDeleted);
                state = result!;
                        
            if (state == null)
            {
                response.Success = false;
                response.Message = $"Sorry! There is no state with this id '{stateId}'";
            }
            else
            {
                var country = await _context.Countries.FindAsync(state.CountryId);
                response.Data = new StateResponse()
                {
                    CountryId = country!.Id,
                    CountryName = country!.Name,
                    State = state
                };
            }

            return response;
        }

        public async Task<ServiceResponse<StateReadDto>> GetState(int stateId)
        {
            StateReadDto? state = null;

            var result = await _context.States
                //.Include(s => s.LGAs.Where(l => l.IsActive && !l.IsDeleted).OrderBy(l => l.Name))
                .FirstOrDefaultAsync(s => s.Id == stateId && s.IsActive && !s.IsDeleted);

            if (result == null)
            {
                return new ServiceResponse<StateReadDto>()
                {
                    Success = false,
                    Message = $"Sorry! There is no state with this id '{stateId}'",
                    Data = null
                };
            }

            state = _mapper.Map<StateReadDto>(result);
            return new ServiceResponse<StateReadDto>()
            {
                Data = state
            };
        }

        public async Task<ServiceResponse<State>> CreateState(State state)
        {
            _context.States.Add(state);
            await _context.SaveChangesAsync();
            return new ServiceResponse<State> { Data = state };
        }

        public async Task<ServiceResponse<State>> UpdateState(State state)
        {
            var dbState = await _context.States.FindAsync(state.Id);
            if (dbState == null)
            {
                return new ServiceResponse<State>
                {
                    Success = false,
                    Message = "State not found."
                };
            }

            dbState.Name = state.Name;
            dbState.IsActive = state.IsActive;
            dbState.IsDeleted = state.IsDeleted;

            foreach (var lga in state.LGAs)
            {
                var dblga = await _context.LGAs.FindAsync(lga.Id);
                if (dblga == null)
                {
                    //Add new LGA
                    _context.LGAs.Add(lga);
                }
                else
                {
                    //Update a particular LGA
                    dblga.StateId = lga.StateId;
                    dblga.Name = lga.Name;
                    dblga.IsActive = lga.IsActive;
                    dblga.IsDeleted = lga.IsDeleted;
                }
            }

            await _context.SaveChangesAsync();
            return new ServiceResponse<State> { Data = state };
        }

        public async Task<ServiceResponse<bool>> DeleteState(int stateId)
        {
            var dbState = await _context.States.FindAsync(stateId);
            if (dbState == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,                    
                    Message = "State not found."
                };
            }

            dbState.IsDeleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }
    }
}
