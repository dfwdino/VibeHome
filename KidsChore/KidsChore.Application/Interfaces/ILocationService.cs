using KidsChore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KidsChore.Application.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Location>> GetAllAsync();
        Task<Location?> GetByIdAsync(int id);
        Task AddAsync(Location location);
        Task UpdateAsync(Location location);
        Task DeleteAsync(int id); // Soft delete
    }
} 