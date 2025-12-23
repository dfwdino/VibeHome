using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using VibeHome.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace VibeHome.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly VibeHomeDbContext _context;
        public ReportService(VibeHomeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeeklyEarningsReport>> GetWeeklyEarningsAsync()
        {
            var completions = await _context.ChoreCompletions
                .Where(c => !c.IsDeleted && c.CompletionDateTime >= DateAndTime.Now.AddDays(-60))
                .Include(c => c.Kid)
                .Include(c => c.ChoreItem)
                .OrderByDescending(c => c.CompletionDateTime)
                //.Take(100) // really should do based off from X date.
                .ToListAsync();

            var grouped = completions
                .GroupBy(c => new
                {
                    c.KidId,
                    Week = ISOWeek.GetWeekOfYear(c.CompletionDateTime),
                    Year = c.CompletionDateTime.Year
                })
                .Select(g =>
                {
                    var weekStart = System.Globalization.ISOWeek.ToDateTime(g.Key.Year, g.Key.Week, DayOfWeek.Monday);
                    return new WeeklyEarningsReport
                    {
                        KidId = g.Key.KidId,
                        KidName = g.First().Kid?.Name ?? "",
                        WeekNumber = g.Key.Week,
                        Year = g.Key.Year,
                        WeekStartDate = weekStart,
                        TotalEarnings = g.Sum(x => x.Price > 0 ? x.Price : (x.ChoreItem?.Price ?? 0)),
                        Completions = g.ToList()
                    };
                })
                .OrderBy(r => r.KidName)
                .ThenBy(r => r.Year)
                .ThenBy(r => r.WeekNumber)
                .ToList();

            return grouped;
        }

        public async Task<IEnumerable<MonthlyEarningsReport>> GetMonthlyEarningsAsync()
        {
            var completions = await _context.ChoreCompletions
                .Where(c => !c.IsDeleted)
                .Include(c => c.Kid)
                .Include(c => c.ChoreItem)
                .ToListAsync();

            var grouped = completions
                .GroupBy(c => new
                {
                    c.KidId,
                    Month = c.CompletionDateTime.Month,
                    Year = c.CompletionDateTime.Year
                })
                .Select(g => new MonthlyEarningsReport
                {
                    KidId = g.Key.KidId,
                    KidName = g.First().Kid?.Name ?? "",
                    Month = g.Key.Month,
                    Year = g.Key.Year,
                    TotalEarnings = g.Sum(x => x.Price > 0 ? x.Price : (x.ChoreItem?.Price ?? 0)),
                    Completions = g.ToList()
                })
                .OrderBy(r => r.KidName)
                .ThenBy(r => r.Year)
                .ThenBy(r => r.Month)
                .ToList();

            return grouped;
        }
    }
} 