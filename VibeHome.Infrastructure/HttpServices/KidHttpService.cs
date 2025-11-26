using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class KidHttpService : IKidService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/Kids";

        public KidHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Kid>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<Kid>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<Kid>();
        }

        public async Task<Kid?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Kid>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task AddAsync(Kid kid)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, kid);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(Kid kid)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({kid.KidId})", kid);
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
