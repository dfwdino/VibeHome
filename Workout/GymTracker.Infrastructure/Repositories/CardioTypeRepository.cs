using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;
using GymTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Infrastructure.Repositories;

public class CardioTypeRepository : ICardioTypeRepository
{
    private readonly ApplicationDbContext _context;

    public CardioTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CardioType>> GetAllAsync()
    {
        return await _context.CardioTypes.ToListAsync();
    }

    public async Task<CardioType?> GetByIdAsync(int id)
    {
        return await _context.CardioTypes.FindAsync(id);
    }

    public async Task AddAsync(CardioType type)
    {
        await _context.CardioTypes.AddAsync(type);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CardioType type)
    {
        _context.CardioTypes.Update(type);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var type = await _context.CardioTypes.FindAsync(id);
        if (type != null)
        {
            _context.CardioTypes.Remove(type);
            await _context.SaveChangesAsync();
        }
    }
} 