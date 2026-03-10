using Microsoft.EntityFrameworkCore;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities.Recipes;
using VibeHome.Infrastructure.Data;

namespace VibeHome.Infrastructure.Services.Recipes
{
    public class RecipeService : IRecipeService
    {
        private readonly IRepository<Recipe> _repo;
        private readonly VibeHomeDbContext _context;

        public RecipeService(IRepository<Recipe> repo, VibeHomeDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<IEnumerable<Recipe>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.OrderBy(r => r.RecipeName);
        }

        public async Task<Recipe?> GetByIdAsync(int id)
        {
            return await _context.Recipes
                .Include(r => r.RecipeIngredients.Where(i => !i.IsDeleted))
                    .ThenInclude(ri => ri.UnitType)
                .Include(r => r.RecipeInstructions.Where(i => !i.IsDeleted))
                .Include(r => r.RecipeFavorites.Where(f => !f.IsDeleted))
                .FirstOrDefaultAsync(r => r.RecipeId == id && !r.IsDeleted);
        }

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
