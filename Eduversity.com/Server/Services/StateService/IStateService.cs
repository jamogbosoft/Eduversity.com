namespace Eduversity.com.Server.Services.StateService
{
  
    public interface IStateService
    {
        Task<ServiceResponse<StatesResponse>> GetAdminStates(int countryId);
        Task<ServiceResponse<List<StateReadDto>>> GetStates(int countryId);
        Task<ServiceResponse<StateResponse>> GetAdminState(int stateId);
        Task<ServiceResponse<StateReadDto>> GetState(int stateId);       
        Task<ServiceResponse<State>> CreateState(State state);
        Task<ServiceResponse<State>> UpdateState(State state);
        Task<ServiceResponse<bool>> DeleteState(int stateId);
    }
}
