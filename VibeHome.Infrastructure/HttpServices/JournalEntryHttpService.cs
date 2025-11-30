using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class JournalEntryHttpService : IJournalEntryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/JournalEntries";

        public JournalEntryHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<JournalEntry>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<JournalEntry>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<JournalEntry>();
        }

        public async Task<JournalEntry?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<JournalEntry>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task AddAsync(JournalEntry entry)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, entry);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(JournalEntry entry)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({entry.Id})", entry);
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
