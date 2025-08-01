using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Application.Interfaces
{
    public interface IChoreItemService
    {
        Task<IEnumerable<ChoreItem>> GetAllAsync();
        Task<ChoreItem?> GetByIdAsync(int id);
        Task AddAsync(ChoreItem item);
        Task UpdateAsync(ChoreItem item);
        Task DeleteAsync(int id); // Soft delete
    }
} 