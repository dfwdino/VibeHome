using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class IngredientHttpService : IIngredientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/Ingredients";

        public IngredientHttpService(HttpClient httpClient) { _httpClient = httpClient; }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<Ingredient>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<Ingredient>();
        }

        public async Task<Ingredient?> GetByIdAsync(int id)
        {
            try { return await _httpClient.GetFromJsonAsync<Ingredient>($"{_endpoint}({id})"); }
            catch (HttpRequestException) { return null; }
        }

        public async Task<Ingredient> AddAndGetAsync(Ingredient ingredient)
        {
            ingredient.CreatedAt = DateTime.Now;
            ingredient.ModifiedAt = DateTime.Now;
            var response = await _httpClient.PostAsJsonAsync(_endpoint, ingredient);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<Ingredient>();
            return created ?? ingredient;
        }

        public async Task AddAsync(Ingredient ingredient)
        {
            ingredient.CreatedAt = DateTime.Now;
            ingredient.ModifiedAt = DateTime.Now;
            var response = await _httpClient.PostAsJsonAsync(_endpoint, ingredient);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(Ingredient ingredient)
        {
            ingredient.ModifiedAt = DateTime.Now;
            ingredient.CreatedAt = DateTime.Now;
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({ingredient.IngredientId})", ingredient);
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
