﻿namespace Eduversity.com.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthService(HttpClient http, AuthenticationStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/change-password", request);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            return response!;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            // return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
            var authState = await _authStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return (user != null && user.Identity != null) ? user.Identity.IsAuthenticated : false;

        }

        public async Task<ServiceResponse<string>> Login(UserLogin request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/login", request);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            return response!;
        }

        public async Task<ServiceResponse<long>> Register(UserRegister request)
        {
            var result = await _http.PostAsJsonAsync("api/auth/register", request);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<long>>();
            return response!;
        }
    }
}