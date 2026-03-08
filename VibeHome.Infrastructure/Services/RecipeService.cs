using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> _repo;

        public RecipeService(IRepository<Recipe> repo) { _repo = repo; }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.OrderBy(r => r.RecipeName);
        }

        public Task<Recipe?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<Recipe> AddAndGetAsync(Recipe recipe)
        {
            await _repo.AddAsync(recipe);
            return recipe; // EF Core populates RecipeId after SaveChanges
        }

        public Task AddAsync(Recipe recipe) => _repo.AddAsync(recipe);
        public Task UpdateAsync(Recipe recipe) => _repo.UpdateAsync(recipe);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
