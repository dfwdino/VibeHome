using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class WeeklyPaymentStatusHttpService : IWeeklyPaymentStatusService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/WeeklyPaymentStatuses";

        public WeeklyPaymentStatusHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeeklyPaymentStatus?> GetByKidAndWeekAsync(int kidId, DateTime weekStartDate)
        {
            var dateString = weekStartDate.ToString("yyyy-MM-dd");
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<WeeklyPaymentStatus>>(
                $"{_endpoint}?$filter=KidId eq {kidId} and WeekStartDate eq {dateString}");
            return response?.Value?.FirstOrDefault();
        }

        public async Task MarkAsPaidAsync(int kidId, DateTime weekStartDate)
        {
            var existing = await GetByKidAndWeekAsync(kidId, weekStartDate);

            if (existing != null)
            {
                existing.IsPaid = true;
                var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({existing.WeeklyPaymentStatusId})", existing);
                response.EnsureSuccessStatusCode();
            }
            else
            {
                var newStatus = new WeeklyPaymentStatus
                {
                    KidId = kidId,
                    WeekStartDate = weekStartDate,
                    IsPaid = true
                };
                var response = await _httpClient.PostAsJsonAsync(_endpoint, newStatus);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task MarkAsUnpaidAsync(int kidId, DateTime weekStartDate)
        {
            var existing = await GetByKidAndWeekAsync(kidId, weekStartDate);

            if (existing != null)
            {
                existing.IsPaid = false;
                var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({existing.WeeklyPaymentStatusId})", existing);
                response.EnsureSuccessStatusCode();
            }
        }

        public async Task<bool> IsPaidAsync(int kidId, DateTime weekStartDate)
        {
            var status = await GetByKidAndWeekAsync(kidId, weekStartDate);
            return status?.IsPaid ?? false;
        }

        private class ODataResponse<T>
        {
            public List<T> Value { get; set; } = new();
        }
    }
}
