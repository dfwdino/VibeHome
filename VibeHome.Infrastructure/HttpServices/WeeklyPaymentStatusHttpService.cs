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
            // OData v4 requires dates in ISO 8601 format without quotes
            var dateString = weekStartDate.ToString("yyyy-MM-dd");
            var url = $"{_endpoint}?$filter=KidId eq {kidId} and WeekStartDate eq {dateString}";

            Console.WriteLine($"[DEBUG] Full URL with filter: {url}");

            var response = await _httpClient.GetFromJsonAsync<ODataResponse<WeeklyPaymentStatus>>(url);
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

        public async Task<IEnumerable<WeeklyPaymentStatus>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<WeeklyPaymentStatus>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<WeeklyPaymentStatus>();
        }

        public async Task<WeeklyPaymentStatus?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<WeeklyPaymentStatus>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task AddAsync(WeeklyPaymentStatus entity)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(WeeklyPaymentStatus entity)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({entity.WeeklyPaymentStatusId})", entity);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_endpoint}({id})");
            response.EnsureSuccessStatusCode();
        }

        private class ODataResponse<T>
        {
            public List<T> Value { get; set; } = new();
        }
    }
}
