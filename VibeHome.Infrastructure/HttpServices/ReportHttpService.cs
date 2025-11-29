using System.Net.Http.Json;
using VibeHome.Application.Interfaces;

namespace VibeHome.Infrastructure.HttpServices
{
    public class ReportHttpService : IReportService
    {
        private readonly HttpClient _httpClient;

        public ReportHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WeeklyEarningsReport>> GetWeeklyEarningsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<WeeklyEarningsReport>>("api/Reports/weekly-earnings");
            return response ?? Enumerable.Empty<WeeklyEarningsReport>();
        }

        public async Task<IEnumerable<MonthlyEarningsReport>> GetMonthlyEarningsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<MonthlyEarningsReport>>("api/Reports/monthly-earnings");
            return response ?? Enumerable.Empty<MonthlyEarningsReport>();
        }
    }
}
