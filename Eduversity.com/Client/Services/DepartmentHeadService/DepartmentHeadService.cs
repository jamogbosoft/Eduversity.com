using Eduversity.com.Shared.Dtos.DepartmentHeadDto;

namespace Eduversity.com.Client.Services.DepartmentHeadService
{
    public class DepartmentHeadService : IDepartmentHeadService
    {
        private readonly HttpClient _http;

        public DepartmentHeadService(HttpClient http)
        {
            _http = http;
        }

        public string Message { get; set; } = string.Empty;
        public int CurrentOptionId { get; set; } = 0;
        public List<DepartmentHeadResponse> DepartmentHeads { get; set; } = new();
        public DepartmentHeadResponse DepartmentHead { get; set; } = new();

        public event Action? DepartmentHeadsChanged;
        public event Action? DepartmentHeadChanged;

        public async Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHead()
        {
            var result = await _http
               .GetFromJsonAsync<ServiceResponse<DepartmentHeadResponse>>("api/departmentheads/hod");
            if (result != null && result.Data != null)
            {
                DepartmentHead = result.Data;
                DepartmentHeadChanged?.Invoke();
            }                     

            return result!;
        }

        public async Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByDepartmentId(int departmentId)
        {
            var result = await _http
               .GetFromJsonAsync<ServiceResponse<DepartmentHeadResponse>>($"api/departmentheads/department/{departmentId}");
            return result!;
        }

        public async Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByHeadId(int headId)
        {
            var result = await _http
              .GetFromJsonAsync<ServiceResponse<DepartmentHeadResponse>>($"api/departmentheads/{headId}");
            return result!;
        }

        public async Task<ServiceResponse<DepartmentHeadResponse>> GetDepartmentHeadByUserId(long userId)
        {
            var result = await _http
             .GetFromJsonAsync<ServiceResponse<DepartmentHeadResponse>>($"api/departmentheads/user/{userId}");
            return result!;
        }

        public async Task GetDepartmentHeads()
        {
            var result = await _http
                  .GetFromJsonAsync<ServiceResponse<List<DepartmentHeadResponse>>>("api/departmentheads/hods");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No department head found.";
                DepartmentHeads = new List<DepartmentHeadResponse>();
            }
            else
            {
                DepartmentHeads = result.Data;
            }

            CurrentOptionId = 0;
            DepartmentHeadsChanged?.Invoke();
        }

        public async Task<DepartmentHeadResponse> AddDepartmentHead(DepartmentHeadRequest request)
        {
            var response = await _http.PostAsJsonAsync("api/departmentheads", request);
            var result = await response.Content
                .ReadFromJsonAsync<ServiceResponse<DepartmentHeadResponse>>();
            if (result == null)
            {
                Message = "Action was not successful.";
                return null!;
            }
            return result.Data!;
        }
        public async Task<DepartmentHeadResponse> UpdateDepartmentHead(int headId, DepartmentHeadRequest request)
        {
            var response = await _http.PutAsJsonAsync($"api/departmentheads/{headId}", request);
            var result = await response.Content
                .ReadFromJsonAsync<ServiceResponse<DepartmentHeadResponse>>();
            if (result == null)
            {
                Message = "Action was not successful.";
                return null!;
            }
            return result.Data!;
        }
    }
}
