using KidsChore.Application.Interfaces;
using KidsChore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace KidsChore.Infrastructure.Services
{
    public class ChoreItemService : IChoreItemService
    {
        private readonly IRepository<ChoreItem> _repo;
        public ChoreItemService(IRepository<ChoreItem> repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<ChoreItem>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.OrderBy(c => c.ChoreName);
        }
        public Task<ChoreItem?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task AddAsync(ChoreItem item) => _repo.AddAsync(item);
        public Task UpdateAsync(ChoreItem item) => _repo.UpdateAsync(item);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
} 