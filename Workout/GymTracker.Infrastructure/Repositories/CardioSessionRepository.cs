using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;
using GymTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Infrastructure.Repositories;

public class CardioSessionRepository : ICardioSessionRepository
{
    private readonly ApplicationDbContext _context;

    public CardioSessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CardioSession>> GetAllAsync()
    {
        return await _context.CardioSessions.ToListAsync();
    }

    public async Task<CardioSession?> GetByIdAsync(int id)
    {
        return await _context.CardioSessions.FindAsync(id);
    }

    public async Task AddAsync(CardioSession session)
    {
        await _context.CardioSessions.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CardioSession session)
    {
        var existingSession = await _context.CardioSessions.FindAsync(session.Id);
        if (existingSession != null)
        {
            _context.Entry(existingSession).State = EntityState.Detached;
        }
        _context.CardioSessions.Update(session);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var session = await _context.CardioSessions.FindAsync(id);
        if (session != null)
        {
            _context.CardioSessions.Remove(session);
            await _context.SaveChangesAsync();
        }
    }
} 