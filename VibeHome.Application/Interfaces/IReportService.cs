using System.Collections.Generic;
using System.Threading.Tasks;
using VibeHome.Domain.Entities;

namespace VibeHome.Application.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<WeeklyEarningsReport>> GetWeeklyEarningsAsync();
        Task<IEnumerable<MonthlyEarningsReport>> GetMonthlyEarningsAsync();
    }

    public class WeeklyEarningsReport
    {
        public int KidId { get; set; }
        public string KidName { get; set; } = string.Empty;
        public int WeekNumber { get; set; }
        public int Year { get; set; }
        public DateTime WeekStartDate { get; set; }
        public decimal TotalEarnings { get; set; }
        public List<ChoreCompletion> Completions { get; set; } = new();
    }

    public class MonthlyEarningsReport
    {
        public int KidId { get; set; }
        public string KidName { get; set; } = string.Empty;
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal TotalEarnings { get; set; }
        public List<ChoreCompletion> Completions { get; set; } = new();
    }
} 