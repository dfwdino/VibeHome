using KidsChore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KidsChore.Application.Interfaces
{
    public interface IKidService
    {
        Task<IEnumerable<Kid>> GetAllAsync();
        Task<Kid?> GetByIdAsync(int id);
        Task AddAsync(Kid kid);
        Task UpdateAsync(Kid kid);
        Task DeleteAsync(int id); // Soft delete
    }
} 