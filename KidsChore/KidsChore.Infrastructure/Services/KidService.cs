using KidsChore.Application.Interfaces;
using KidsChore.Domain.Entities;
using KidsChore.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace KidsChore.Infrastructure.Services
{
    public class KidService : IKidService
    {
        private readonly IRepository<Kid> _repo;
        public KidService(IRepository<Kid> repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<Kid>> GetAllAsync()
        {
            var kids = await _repo.GetAllAsync();
            return kids.OrderBy(k => k.Name);
        }
        public Task<Kid?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task AddAsync(Kid kid) => _repo.AddAsync(kid);
        public Task UpdateAsync(Kid kid) => _repo.UpdateAsync(kid);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
} 