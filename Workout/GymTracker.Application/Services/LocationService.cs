using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;

namespace GymTracker.Application.Services;

public class LocationService
{
    private readonly ILocationRepository _repository;

    public LocationService(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Location?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(Location location)
    {
        // Add validation logic here if needed
        await _repository.AddAsync(location);
    }

    public async Task UpdateAsync(Location location)
    {
        // Add validation logic here if needed
        await _repository.UpdateAsync(location);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
} 