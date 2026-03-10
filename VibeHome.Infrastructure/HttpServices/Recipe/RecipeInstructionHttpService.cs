using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities.Recipes;

namespace VibeHome.Infrastructure.HttpServices.Recipes
{
    public class RecipeInstructionHttpService : IRecipeInstructionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/RecipeInstructions";

        public RecipeInstructionHttpService(HttpClient httpClient) { _httpClient = httpClient; }

        public async Task<IEnumerable<RecipeInstruction>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<RecipeInstruction>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<RecipeInstruction>();
        }

        public async Task<IEnumerable<RecipeInstruction>> GetByRecipeIdAsync(int recipeId)
        {
            var url = $"{_endpoint}?$filter=RecipeId eq {recipeId} and IsDeleted eq false&$orderby=StepNumber";
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<RecipeInstruction>>(url);
            return response?.Value ?? Enumerable.Empty<RecipeInstruction>();
        }

        public async Task<RecipeInstruction?> GetByIdAsync(int id)
        {
            try { return await _httpClient.GetFromJsonAsync<RecipeInstruction>($"{_endpoint}({id})"); }
            catch (HttpRequestException) { return null; }
        }

        public async Task AddAsync(RecipeInstruction recipeInstruction)
        {
            recipeInstruction.CreatedAt = DateTime.Now;
            recipeInstruction.ModifiedAt = DateTime.Now;
            var response = await _httpClient.PostAsJsonAsync(_endpoint, recipeInstruction);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(RecipeInstruction recipeInstruction)
        {
            recipeInstruction.ModifiedAt = DateTime.Now;
            recipeInstruction.CreatedAt = DateTime.Now;
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({recipeInstruction.RecipeInstructionId})", recipeInstruction);
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
