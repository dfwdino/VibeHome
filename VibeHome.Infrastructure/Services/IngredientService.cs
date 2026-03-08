using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IRepository<Ingredient> _repo;

        public IngredientService(IRepository<Ingredient> repo) { _repo = repo; }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.OrderBy(i => i.IngredientName);
        }

        public Task<Ingredient?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);

        public async Task<Ingredient> AddAndGetAsync(Ingredient ingredient)
        {
            await _repo.AddAsync(ingredient);
            return ingredient; // EF Core populates IngredientId after SaveChanges
        }

        public Task AddAsync(Ingredient ingredient) => _repo.AddAsync(ingredient);
        public Task UpdateAsync(Ingredient ingredient) => _repo.UpdateAsync(ingredient);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
