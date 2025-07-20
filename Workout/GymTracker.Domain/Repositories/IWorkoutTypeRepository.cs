using GymTracker.Domain.Models;

namespace GymTracker.Domain.Repositories;

public interface IWorkoutTypeRepository
{
    Task<IEnumerable<WorkoutType>> GetAllAsync();
    Task<WorkoutType?> GetByIdAsync(int id);
    Task AddAsync(WorkoutType type);
    Task UpdateAsync(WorkoutType type);
    Task DeleteAsync(int id);
} 