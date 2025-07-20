using GymTracker.Domain.Models;

namespace GymTracker.Domain.Repositories;

public interface IWeightTrainingSessionRepository
{
    Task<IEnumerable<WeightTrainingSession>> GetAllAsync();
    Task<WeightTrainingSession?> GetByIdAsync(int id);
    Task AddAsync(WeightTrainingSession session);
    Task UpdateAsync(WeightTrainingSession session);
    Task DeleteAsync(int id);
} 