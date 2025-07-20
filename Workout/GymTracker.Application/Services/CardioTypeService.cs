using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;

namespace GymTracker.Application.Services;

public class CardioTypeService
{
    private readonly ICardioTypeRepository _repository;

    public CardioTypeService(ICardioTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CardioType>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<CardioType?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task AddAsync(CardioType type)
    {
        // Add validation logic here if needed
        await _repository.AddAsync(type);
    }

    public async Task UpdateAsync(CardioType type)
    {
        // Add validation logic here if needed
        await _repository.UpdateAsync(type);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
} 