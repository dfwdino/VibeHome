using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Application.Interfaces
{
    public interface IChoreCompletionService
    {
        Task<IEnumerable<ChoreCompletion>> GetAllAsync();
        Task<ChoreCompletion?> GetByIdAsync(int id);
        Task AddAsync(ChoreCompletion completion);
        Task UpdateAsync(ChoreCompletion completion);
        Task DeleteAsync(int id); // Soft delete
    }
} 