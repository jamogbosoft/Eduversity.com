namespace Eduversity.com.Client.Services.StateService
{
    public interface IStateService
    {
        List<StateReadDto> States { get; set; }
        StatesResponse AdminResponse { get; set; }
        string Message { get; set; }
        Task<ServiceResponse<StateResponse>> GetAdminState(int stateId);
        Task<ServiceResponse<StateReadDto>> GetState(int stateId);
        Task GetAdminStates(int countryId);
        Task GetStates(int countryId);
        Task<State> CreateState(State state);
        Task<State> UpdateState(State state);
        Task DeleteState(State state);
    }
}
