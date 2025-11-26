using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class KidsChoreLocationHttpService : IKidsChoreLocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/KidsChoreLocations";

        public KidsChoreLocationHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<KidsChoreLocation>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<KidsChoreLocation>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<KidsChoreLocation>();
        }

        public async Task<KidsChoreLocation?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<KidsChoreLocation>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task AddAsync(KidsChoreLocation location)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, location);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(KidsChoreLocation location)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({location.LocationId})", location);
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
