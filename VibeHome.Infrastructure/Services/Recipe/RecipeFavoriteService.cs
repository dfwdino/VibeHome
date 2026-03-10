using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities.Recipes;

namespace VibeHome.Infrastructure.Services.Recipes
{
    public class RecipeFavoriteService : IRecipeFavoriteService
    {
        private readonly IRepository<RecipeFavorite> _repo;

        public RecipeFavoriteService(IRepository<RecipeFavorite> repo) { _repo = repo; }

        public async Task<IEnumerable<RecipeFavorite>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.OrderByDescending(f => f.CreatedAt);
        }

        public Task<RecipeFavorite?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<RecipeFavorite?> GetByRecipeIdAsync(int recipeId)
        {
            var items = await _repo.GetAllAsync();
            return items.Where(f => f.RecipeId == recipeId && !f.IsDeleted)
                        .OrderByDescending(f => f.CreatedAt)
                        .FirstOrDefault();
        }

        public Task AddAsync(RecipeFavorite recipeFavorite) => _repo.AddAsync(recipeFavorite);
        public Task UpdateAsync(RecipeFavorite recipeFavorite) => _repo.UpdateAsync(recipeFavorite);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
