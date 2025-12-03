using System.Net.Http.Json;
using System.Text.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class ChoreItemHttpService : IChoreItemService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/ChoreItems";

        public ChoreItemHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ChoreItem>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<ChoreItem>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<ChoreItem>();
        }

        public async Task<ChoreItem?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ChoreItem>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task AddAsync(ChoreItem choreItem)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, choreItem);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(ChoreItem choreItem)
        {
            //Need to fix this
            choreItem.ModifiedAt = DateTime.UtcNow;
            choreItem.CreatedAt = DateTime.UtcNow;

            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({choreItem.ChoreItemId})", choreItem);
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
