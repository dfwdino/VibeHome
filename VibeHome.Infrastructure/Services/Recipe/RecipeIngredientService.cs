using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities.Recipes;

namespace VibeHome.Infrastructure.Services.Recipes
{
    public class RecipeIngredientService : IRecipeIngredientService
    {
        private readonly IRepository<RecipeIngredient> _repo;

        public RecipeIngredientService(IRepository<RecipeIngredient> repo) { _repo = repo; }

        public async Task<IEnumerable<RecipeIngredient>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.OrderBy(ri => ri.SortOrder);
        }

        public async Task<IEnumerable<RecipeIngredient>> GetByRecipeIdAsync(int recipeId)
        {
            var items = await _repo.GetAllAsync();
            return items
                .Where(ri => ri.RecipeId == recipeId && !ri.IsDeleted)
                .OrderBy(ri => ri.SortOrder);
        }

        public Task<RecipeIngredient?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task AddAsync(RecipeIngredient recipeIngredient) => _repo.AddAsync(recipeIngredient);
        public Task UpdateAsync(RecipeIngredient recipeIngredient) => _repo.UpdateAsync(recipeIngredient);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
