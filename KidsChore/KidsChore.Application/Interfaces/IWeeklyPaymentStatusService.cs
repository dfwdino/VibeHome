using KidsChore.Domain.Entities;

namespace KidsChore.Application.Interfaces
{
    public interface IWeeklyPaymentStatusService
    {
        Task<WeeklyPaymentStatus?> GetByKidAndWeekAsync(int kidId, DateTime weekStartDate);
        Task MarkAsPaidAsync(int kidId, DateTime weekStartDate);
        Task MarkAsUnpaidAsync(int kidId, DateTime weekStartDate);
        Task<bool> IsPaidAsync(int kidId, DateTime weekStartDate);
    }
} 