namespace Eduversity.com.Client.Services.DepartmentOptionService
{
    public class DepartmentOptionService : IDepartmentOptionService
    {
        private readonly HttpClient _http;

        public DepartmentOptionService(HttpClient http)
        {
            _http = http;
        }

        public event Action? DepartmentOptionsChanged;
        public List<DepartmentOptionReadDto> DepartmentOptions { get; set; } = new List<DepartmentOptionReadDto>();
        public DepartmentOptionsResponse AdminResponse { get; set; } = new DepartmentOptionsResponse();
        public string Message { get; set; } = string.Empty;

        public async Task<DepartmentOption> CreateDepartmentOption(DepartmentOption option)
        {
            var result = await _http.PostAsJsonAsync("api/departmentoptions", option);
            var newOption = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<DepartmentOption>>())!.Data;
            return newOption!;
        }

        public async Task DeleteDepartmentOption(DepartmentOption option)
        {
            var result = await _http.DeleteAsync($"api/departmentoptions/{option.Id}");
        }

        public async Task<ServiceResponse<DepartmentOptionResponse>> GetAdminDepartmentOption(int optionId)
        {
            var result = await _http
              .GetFromJsonAsync<ServiceResponse<DepartmentOptionResponse>>($"api/departmentoptions/admin-oId/{optionId}");
            return result!;
        }

        public async Task GetAdminDepartmentOptions(int departmentId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<DepartmentOptionsResponse>>($"api/departmentoptions/admin-dId/{departmentId}");

            if (result != null)
            {
                if (result.Success)
                {
                    if (result.Data != null)
                    {
                        AdminResponse = result.Data;
                    }
                    if (AdminResponse.Options == null || AdminResponse.Options.Count == 0)
                    {
                        Message = $"There is no option in '{AdminResponse.DepartmentName}'!";
                    }
                }
                else { Message = result.Message; }
            }
            else { Message = "Nothing was returned."; }
        }

        public async Task<ServiceResponse<DepartmentOptionReadDto>> GetDepartmentOption(int optionId)
        {
            var result = await _http
              .GetFromJsonAsync<ServiceResponse<DepartmentOptionReadDto>>($"api/departmentoptions/user-oId/{optionId}");
            return result!;            
        }

        public async Task GetDepartmentOptions(int departmentId)
        {
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<List<DepartmentOptionReadDto>>>($"api/departmentoptions/user-dId/{departmentId}");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No option found.";
                DepartmentOptions = new List<DepartmentOptionReadDto>();
                return;
            }
            DepartmentOptions = result.Data;

            DepartmentOptionsChanged?.Invoke();
        }

        public async Task GetDepartmentOptions()
        {
            var result = await _http
                 .GetFromJsonAsync<ServiceResponse<List<DepartmentOptionReadDto>>>($"api/departmentoptions/admin");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No option found.";
                DepartmentOptions = new List<DepartmentOptionReadDto>();
                return;
            }
            DepartmentOptions = result.Data;

            DepartmentOptionsChanged?.Invoke();
        }

        public async Task<DepartmentOption> UpdateDepartmentOption(DepartmentOption option)
        {
            var result = await _http.PutAsJsonAsync($"api/departmentoptions", option);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<DepartmentOption>>();
            return content!.Data!;
        }
    }
}
