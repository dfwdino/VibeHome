using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;

namespace GymTracker.Application.Services;

public class WorkoutTypeService
{
    private readonly IWorkoutTypeRepository _repository;

    public WorkoutTypeService(IWorkoutTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WorkoutType>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<WorkoutType?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(WorkoutType type)
    {
        // Add validation logic here if needed
        await _repository.AddAsync(type);
    }

    public async Task UpdateAsync(WorkoutType type)
    {
        // Add validation logic here if needed
        await _repository.UpdateAsync(type);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
} 