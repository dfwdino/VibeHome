using VibeHome.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VibeHome.Application.Interfaces
{
    public interface IWorkoutTypeService
    {
        Task<IEnumerable<WorkoutType>> GetAllAsync();
        Task<WorkoutType?> GetByIdAsync(int id);
        Task AddAsync(WorkoutType workoutType);
        Task UpdateAsync(WorkoutType workoutType);
        Task DeleteAsync(int id);
    }
} 