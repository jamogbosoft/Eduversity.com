namespace Eduversity.com.Client.Services.StateService
{
    public class StateService : IStateService
    {
        private readonly HttpClient _http;

        public StateService(HttpClient http)
        {
            _http = http;
        }
        public List<StateReadDto> States { get; set; } = new List<StateReadDto>();
        public StatesResponse AdminResponse { get; set; } = new StatesResponse();
        public string Message { get; set; } = string.Empty;

        public async Task GetAdminStates(int countryId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<StatesResponse>>($"api/states/admin-cId/{countryId}");
            
            if (result != null)
            {
                if (result.Success)
                {
                    if (result.Data != null)
                    {
                        AdminResponse = result.Data;
                    }
                    if (AdminResponse.States == null || AdminResponse.States.Count == 0)
                    {
                        Message = $"There is no state in '{AdminResponse.CountryName}'!";
                    }
                }
                else { Message = result.Message; }
            }
            else { Message = "Nothing was returned."; }           
        }

        public async Task GetStates(int countryId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<StateReadDto>>>($"api/states/user-cId/{countryId}");

            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No state found.";
                States = new List<StateReadDto>();
                return;
            }
            States = result.Data;
        }

        public async Task<ServiceResponse<StateResponse>> GetAdminState(int stateId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<StateResponse>>($"api/states/admin-sId/{stateId}");
            return result!;
        }

        public async Task<ServiceResponse<StateReadDto>> GetState(int stateId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<StateReadDto>>($"api/states/user-sId/{stateId}");
            return result!;
        }     

        public async Task<State> CreateState(State state)
        {
            var result = await _http.PostAsJsonAsync("api/states", state);
            var newState = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<State>>())!.Data;
            return newState!;
        }
               
        public async Task<State> UpdateState(State state)
        {
            var response = await _http.PutAsJsonAsync($"api/states", state);
            var result = await response.Content
                .ReadFromJsonAsync<ServiceResponse<State>>();

            if (result == null || result.Data == null)
            {
                return null!;
            }
            return result.Data;
        }
        public async Task DeleteState(State state)
        {
            var result = await _http.DeleteAsync($"api/states/{state.Id}");
        }
    }
}
