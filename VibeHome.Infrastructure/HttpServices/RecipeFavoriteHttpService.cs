using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class RecipeFavoriteHttpService : IRecipeFavoriteService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/RecipeFavorites";

        public RecipeFavoriteHttpService(HttpClient httpClient) { _httpClient = httpClient; }

        public async Task<IEnumerable<RecipeFavorite>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<RecipeFavorite>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<RecipeFavorite>();
        }

        public async Task<RecipeFavorite?> GetByIdAsync(int id)
        {
            try { return await _httpClient.GetFromJsonAsync<RecipeFavorite>($"{_endpoint}({id})"); }
            catch (HttpRequestException) { return null; }
        }

        public async Task AddAsync(RecipeFavorite recipeFavorite)
        {
            recipeFavorite.CreatedAt = DateTime.Now;
            recipeFavorite.ModifiedAt = DateTime.Now;
            var response = await _httpClient.PostAsJsonAsync(_endpoint, recipeFavorite);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(RecipeFavorite recipeFavorite)
        {
            recipeFavorite.ModifiedAt = DateTime.Now;
            recipeFavorite.CreatedAt = DateTime.Now;
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({recipeFavorite.RecipeFavoriteId})", recipeFavorite);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_endpoint}({id})");
            response.EnsureSuccessStatusCode();
        }

        private class ODataResponse<T> { public List<T> Value { get; set; } = new(); }
    }
}
