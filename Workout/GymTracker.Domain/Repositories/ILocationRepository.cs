using GymTracker.Domain.Models;

namespace GymTracker.Domain.Repositories;

public interface ILocationRepository
{
    Task<IEnumerable<Location>> GetAllAsync();
    Task<Location?> GetByIdAsync(int id);
    Task AddAsync(Location location);
    Task UpdateAsync(Location location);
    Task DeleteAsync(int id);
} 