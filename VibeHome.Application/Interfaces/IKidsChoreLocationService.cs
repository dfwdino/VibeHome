using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Application.Interfaces
{
    public interface IKidsChoreLocationService
    {
        Task<IEnumerable<KidsChoreLocation>> GetAllAsync();
        Task<KidsChoreLocation?> GetByIdAsync(int id);
        Task AddAsync(KidsChoreLocation location);
        Task UpdateAsync(KidsChoreLocation location);
        Task DeleteAsync(int id); // Soft delete
    }
} 