using GymTracker.Domain.Models;
using GymTracker.Domain.Repositories;
using GymTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GymTracker.Infrastructure.Repositories;

public class WorkoutTypeRepository : IWorkoutTypeRepository
{
    private readonly ApplicationDbContext _context;

    public WorkoutTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WorkoutType>> GetAllAsync()
    {
        return await _context.WorkoutTypes.ToListAsync();
    }

    public async Task<WorkoutType?> GetByIdAsync(int id)
    {
        return await _context.WorkoutTypes.FindAsync(id);
    }

    public async Task AddAsync(WorkoutType type)
    {
        await _context.WorkoutTypes.AddAsync(type);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WorkoutType type)
    {
        _context.WorkoutTypes.Update(type);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var type = await _context.WorkoutTypes.FindAsync(id);
        if (type != null)
        {
            _context.WorkoutTypes.Remove(type);
            await _context.SaveChangesAsync();
        }
    }
} 