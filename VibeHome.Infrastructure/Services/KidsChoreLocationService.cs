using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace VibeHome.Infrastructure.Services
{
    public class KidsChoreLocationService : IKidsChoreLocationService
    {
        private readonly IRepository<KidsChoreLocation> _repo;
        public KidsChoreLocationService(IRepository<KidsChoreLocation> repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<KidsChoreLocation>> GetAllAsync()
        {
            var locations = await _repo.GetAllAsync();
            return locations.OrderBy(l => l.LocationName);
        }
        public Task<KidsChoreLocation?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task AddAsync(KidsChoreLocation location) => _repo.AddAsync(location);
        public Task UpdateAsync(KidsChoreLocation location) => _repo.UpdateAsync(location);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
} 