using KidsChore.Application.Interfaces;
using KidsChore.Domain.Entities;
using KidsChore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace KidsChore.Infrastructure.Services
{
    public class ReportService : IReportService
    {
        private readonly KidsChoreDbContext _context;
        public ReportService(KidsChoreDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WeeklyEarningsReport>> GetWeeklyEarningsAsync()
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
    }
} 