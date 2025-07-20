using GymTracker.Domain.Models;

namespace GymTracker.Domain.Repositories;

public interface ICardioSessionRepository
{
    Task<IEnumerable<CardioSession>> GetAllAsync();
    Task<CardioSession?> GetByIdAsync(int id);
    Task AddAsync(CardioSession session);
    Task UpdateAsync(CardioSession session);
    Task DeleteAsync(int id);
} 