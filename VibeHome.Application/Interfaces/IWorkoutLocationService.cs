using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Application.Interfaces
{
    public interface IWorkoutLocationService
    {
        Task<IEnumerable<WorkoutLocation>> GetAllAsync();
        Task<WorkoutLocation?> GetByIdAsync(int id);
        Task AddAsync(WorkoutLocation location);
        Task UpdateAsync(WorkoutLocation location);
        Task DeleteAsync(int id);
    }
} 