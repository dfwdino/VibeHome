using KidsChore.Application.Interfaces;
using KidsChore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace KidsChore.Infrastructure.Services
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> _repo;
        public LocationService(IRepository<Location> repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            var locations = await _repo.GetAllAsync();
            return locations.OrderBy(l => l.LocationName);
        }
        public Task<Location?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task AddAsync(Location location) => _repo.AddAsync(location);
        public Task UpdateAsync(Location location) => _repo.UpdateAsync(location);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
} 