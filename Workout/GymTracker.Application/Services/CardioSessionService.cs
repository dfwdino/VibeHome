using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;

namespace GymTracker.Application.Services;

public class CardioSessionService
{
    private readonly ICardioSessionRepository _repository;

    public CardioSessionService(ICardioSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CardioSession>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<CardioSession?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(CardioSession session)
    {
        // Add validation logic here if needed
        await _repository.AddAsync(session);
    }

    public async Task UpdateAsync(CardioSession session)
    {
        // Add validation logic here if needed
        await _repository.UpdateAsync(session);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
} 