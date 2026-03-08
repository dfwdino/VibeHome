using VibeHome.Domain.Entities;

namespace VibeHome.Application.Interfaces
{
    public interface IUnitTypeService
    {
        Task<IEnumerable<UnitType>> GetAllAsync();
        Task<UnitType?> GetByIdAsync(int id);
        Task AddAsync(UnitType unitType);
        Task UpdateAsync(UnitType unitType);
        Task DeleteAsync(int id);
    }
}
