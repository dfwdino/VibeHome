using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.Services
{
    public class UnitTypeService : IUnitTypeService
    {
        private readonly IRepository<UnitType> _repo;

        public UnitTypeService(IRepository<UnitType> repo) { _repo = repo; }

        public async Task<IEnumerable<UnitType>> GetAllAsync()
        {
            var items = await _repo.GetAllAsync();
            return items.OrderBy(u => u.UnitName);
        }

        public Task<UnitType?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
        public Task AddAsync(UnitType unitType) => _repo.AddAsync(unitType);
        public Task UpdateAsync(UnitType unitType) => _repo.UpdateAsync(unitType);
        public Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
