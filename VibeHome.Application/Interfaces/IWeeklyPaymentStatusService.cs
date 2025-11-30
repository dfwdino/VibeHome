using VibeHome.Domain.Entities;

namespace VibeHome.Application.Interfaces
{
    public interface IWeeklyPaymentStatusService
    {
        // Standard CRUD methods
        Task<IEnumerable<WeeklyPaymentStatus>> GetAllAsync();
        Task<WeeklyPaymentStatus?> GetByIdAsync(int id);
        Task AddAsync(WeeklyPaymentStatus entity);
        Task UpdateAsync(WeeklyPaymentStatus entity);
        Task DeleteAsync(int id);

        // Custom methods
        Task<WeeklyPaymentStatus?> GetByKidAndWeekAsync(int kidId, DateTime weekStartDate);
        Task MarkAsPaidAsync(int kidId, DateTime weekStartDate);
        Task MarkAsUnpaidAsync(int kidId, DateTime weekStartDate);
        Task<bool> IsPaidAsync(int kidId, DateTime weekStartDate);
    }
} 