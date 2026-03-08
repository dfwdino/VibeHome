using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class RecipeIngredientHttpService : IRecipeIngredientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/RecipeIngredients";

        public RecipeIngredientHttpService(HttpClient httpClient) { _httpClient = httpClient; }

        public async Task<IEnumerable<RecipeIngredient>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<RecipeIngredient>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<RecipeIngredient>();
        }

        public async Task<IEnumerable<RecipeIngredient>> GetByRecipeIdAsync(int recipeId)
        {
            var url = $"{_endpoint}?$filter=RecipeId eq {recipeId} and IsDeleted eq false&$orderby=SortOrder";
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<RecipeIngredient>>(url);
            return response?.Value ?? Enumerable.Empty<RecipeIngredient>();
        }

        public async Task<RecipeIngredient?> GetByIdAsync(int id)
        {
            try { return await _httpClient.GetFromJsonAsync<RecipeIngredient>($"{_endpoint}({id})"); }
            catch (HttpRequestException) { return null; }
        }

        public async Task AddAsync(RecipeIngredient recipeIngredient)
        {
            recipeIngredient.CreatedAt = DateTime.Now;
            recipeIngredient.ModifiedAt = DateTime.Now;
            var response = await _httpClient.PostAsJsonAsync(_endpoint, recipeIngredient);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(RecipeIngredient recipeIngredient)
        {
            recipeIngredient.ModifiedAt = DateTime.Now;
            recipeIngredient.CreatedAt = DateTime.Now;
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({recipeIngredient.RecipeIngredientId})", recipeIngredient);
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
