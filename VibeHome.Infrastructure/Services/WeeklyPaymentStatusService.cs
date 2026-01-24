using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using VibeHome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace VibeHome.Infrastructure.Services
{
    public class WeeklyPaymentStatusService : IWeeklyPaymentStatusService
    {
        private readonly VibeHomeDbContext _context;

        public WeeklyPaymentStatusService(VibeHomeDbContext context)
        {
            _context = context;
        }

        // Standard CRUD methods
        public async Task<IEnumerable<WeeklyPaymentStatus>> GetAllAsync()
        {
            return await _context.WeeklyPaymentStatuses.ToListAsync();
        }

        public async Task<WeeklyPaymentStatus?> GetByIdAsync(int id)
        {
            return await _context.WeeklyPaymentStatuses.AsNoTracking().FirstAsync(mm => mm.WeeklyPaymentStatusId == id);
        }

        public async Task AddAsync(WeeklyPaymentStatus entity)
        {
            _context.WeeklyPaymentStatuses.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(WeeklyPaymentStatus entity)
        {
            _context.WeeklyPaymentStatuses.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.WeeklyPaymentStatuses.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        // Custom methods
        public async Task<WeeklyPaymentStatus?> GetByKidAndWeekAsync(int kidId, DateTime weekStartDate)
        {
            return await _context.WeeklyPaymentStatuses
                .FirstOrDefaultAsync(w => w.KidId == kidId && w.WeekStartDate == weekStartDate);
        }

        public async Task MarkAsPaidAsync(int kidId, DateTime weekStartDate)
        {
            var existing = await GetByKidAndWeekAsync(kidId, weekStartDate);
            
            if (existing != null)
            {
                existing.IsPaid = true;
                _context.WeeklyPaymentStatuses.Update(existing);
            }
            else
            {
                var newStatus = new WeeklyPaymentStatus
                {
                    KidId = kidId,
                    WeekStartDate = weekStartDate,
                    IsPaid = true
                };
                _context.WeeklyPaymentStatuses.Add(newStatus);
            }
            
            await _context.SaveChangesAsync();
        }

        public async Task MarkAsUnpaidAsync(int kidId, DateTime weekStartDate)
        {
            var existing = await GetByKidAndWeekAsync(kidId, weekStartDate);
            
            if (existing != null)
            {
                existing.IsPaid = false;
                _context.WeeklyPaymentStatuses.Update(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsPaidAsync(int kidId, DateTime weekStartDate)
        {
            var status = await GetByKidAndWeekAsync(kidId, weekStartDate);
            return status?.IsPaid ?? false;
        }
    }
} 