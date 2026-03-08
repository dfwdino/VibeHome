using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.Services
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
        public Task AddAsync(RecipeFavorite recipeFavorite) => _repo.AddAsync(recipeFavorite);
        public Task UpdateAsync(RecipeFavorite recipeFavorite) => _repo.UpdateAsync(recipeFavorite);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
