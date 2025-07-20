using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;
using GymTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Infrastructure.Repositories;

public class WeightTrainingSessionRepository : IWeightTrainingSessionRepository
{
    private readonly ApplicationDbContext _context;

    public WeightTrainingSessionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeightTrainingSession>> GetAllAsync()
    {
        return await _context.WeightTrainingSessions.ToListAsync();
    }

    public async Task<WeightTrainingSession?> GetByIdAsync(int id)
    {
        return await _context.WeightTrainingSessions.FindAsync(id);
    }

    public async Task AddAsync(WeightTrainingSession session)
    {
        await _context.WeightTrainingSessions.AddAsync(session);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WeightTrainingSession session)
    {
        var existingSession = await _context.WeightTrainingSessions.FindAsync(session.Id);
        if (existingSession != null)
        {
            _context.Entry(existingSession).State = EntityState.Detached;
        }
        _context.WeightTrainingSessions.Update(session);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var session = await _context.WeightTrainingSessions.FindAsync(id);
        if (session != null)
        {
            _context.WeightTrainingSessions.Remove(session);
            await _context.SaveChangesAsync();
        }
    }
} 