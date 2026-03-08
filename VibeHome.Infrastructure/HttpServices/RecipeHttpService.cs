using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class RecipeHttpService : IRecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/Recipes";

        public RecipeHttpService(HttpClient httpClient) { _httpClient = httpClient; }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<Recipe>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<Recipe>();
        }

        public async Task<Recipe?> GetByIdAsync(int id)
        {
            try { return await _httpClient.GetFromJsonAsync<Recipe>($"{_endpoint}({id})"); }
            catch (HttpRequestException) { return null; }
        }

        public async Task<Recipe> AddAndGetAsync(Recipe recipe)
        {
            recipe.CreatedAt = DateTime.Now;
            recipe.ModifiedAt = DateTime.Now;
            var response = await _httpClient.PostAsJsonAsync(_endpoint, recipe);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<Recipe>();
            return created ?? recipe;
        }

        public async Task AddAsync(Recipe recipe)
        {
            recipe.CreatedAt = DateTime.Now;
            recipe.ModifiedAt = DateTime.Now;
            var response = await _httpClient.PostAsJsonAsync(_endpoint, recipe);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(Recipe recipe)
        {
            recipe.ModifiedAt = DateTime.Now;
            recipe.CreatedAt = DateTime.Now;
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({recipe.RecipeId})", recipe);
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
