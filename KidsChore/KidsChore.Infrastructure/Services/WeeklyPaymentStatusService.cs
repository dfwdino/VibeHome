using KidsChore.Application.Interfaces;
using KidsChore.Domain.Entities;
using KidsChore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace KidsChore.Infrastructure.Services
{
    public class WeeklyPaymentStatusService : IWeeklyPaymentStatusService
    {
        private readonly KidsChoreDbContext _context;

        public WeeklyPaymentStatusService(KidsChoreDbContext context)
        {
            _context = context;
        }

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