using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;

namespace GymTracker.Application.Services;

public class WeightTrainingSessionService
{
    private readonly IWeightTrainingSessionRepository _repository;

    public WeightTrainingSessionService(IWeightTrainingSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WeightTrainingSession>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<WeightTrainingSession?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(WeightTrainingSession session)
    {
        // Add validation logic here if needed
        await _repository.AddAsync(session);
    }

    public async Task UpdateAsync(WeightTrainingSession session)
    {
        // Add validation logic here if needed
        await _repository.UpdateAsync(session);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
} 