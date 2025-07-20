using GymTracker.Domain.Models;

namespace GymTracker.Domain.Repositories;

public interface ICardioTypeRepository
{
    Task<IEnumerable<CardioType>> GetAllAsync();
    Task<CardioType?> GetByIdAsync(int id);
    Task AddAsync(CardioType type);
    Task UpdateAsync(CardioType type);
    Task DeleteAsync(int id);
} 