using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class UserHttpService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/Users";

        public UserHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<User>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<User>();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<User>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<User>>($"{_endpoint}?$filter=Username eq '{username}'");
            return response?.Value?.FirstOrDefault();
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await GetByUsernameAsync(username);
            if (user != null && user.PasswordHash == password)
            {
                return user;
            }
            return null;
        }

        public async Task AddAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, user);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({user.UserId})", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_endpoint}({id})");
            response.EnsureSuccessStatusCode();
        }

        private class ODataResponse<T>
        {
            public List<T> Value { get; set; } = new();
        }
    }
}
