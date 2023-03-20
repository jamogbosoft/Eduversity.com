namespace Eduversity.com.Client.Services.LGAService
{
    public class LGAService : ILGAService
    {
        private readonly HttpClient _http;

        public LGAService(HttpClient http)
        {
            _http = http;
        }
        public List<LGAReadDto> LGAs { get; set; } = new List<LGAReadDto>();
        public LGAsResponse AdminResponse { get; set; } = new LGAsResponse();
        public string Message { get; set; } = string.Empty;
               
        public async Task GetAdminLGAs(int stateId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<LGAsResponse>>($"api/lgas/admin-sId/{stateId}");

            if (result != null)
            {
                if (result.Success)
                {
                    if (result.Data != null)
                    {
                        AdminResponse = result.Data;
                    }
                    if (AdminResponse.LGAs == null || AdminResponse.LGAs.Count == 0)
                    {
                        Message = $"There is no LGA in '{AdminResponse.StateName}'!";
                    }
                }
                else { Message = result.Message; }
            }
            else { Message = "Nothing was returned."; }
        }

        public async Task<ServiceResponse<LGAResponse>> GetAdminLGA(int lgaId)
        {
            var result = await _http
               .GetFromJsonAsync<ServiceResponse<LGAResponse>>($"api/lgas/admin-lId/{lgaId}");
            return result!;
        }

        public async Task<ServiceResponse<LGAReadDto>> GetLGA(int lgaId)
        {
            var result = await _http
               .GetFromJsonAsync<ServiceResponse<LGAReadDto>>($"api/lgas/user-lId/{lgaId}");
            return result!;
        }

        public async Task GetLGAs(int stateId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<LGAReadDto>>>($"api/lgas/user-sId/{stateId}");
            if (result == null || result.Data == null || result.Data.Count == 0)
            {
                Message = "No LGA found.";
                LGAs = new List<LGAReadDto>();
                return;
            }
            LGAs = result.Data;
        }

        public async Task<LGA> UpdateLGA(LGA lga)
        {
            var result = await _http.PutAsJsonAsync("api/lgas", lga);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<LGA>>();
            return content!.Data!;
        }
        public async Task<LGA> CreateLGA(LGA lga)
        {
            var response = await _http.PostAsJsonAsync("api/lgas", lga);
            var result = await response.Content
                .ReadFromJsonAsync<ServiceResponse<LGA>>();
            if (result == null || result.Data == null)
            {
                return null!;
            }
            return result.Data;
        }
        public async Task DeleteLGA(LGA lga)
        {
            var result = await _http.DeleteAsync($"api/lgas/{lga.Id}");
        }
    }
}
