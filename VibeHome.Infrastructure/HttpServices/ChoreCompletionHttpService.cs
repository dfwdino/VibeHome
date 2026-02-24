using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class ChoreCompletionHttpService : IChoreCompletionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/ChoreCompletions";

        public ChoreCompletionHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ChoreCompletion>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<ChoreCompletion>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<ChoreCompletion>();
        }

        public async Task<ChoreCompletion?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ChoreCompletion>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task AddAsync(ChoreCompletion choreCompletion)
        {
            var response = await _httpClient.PostAsJsonAsync<ChoreCompletion>(_endpoint, choreCompletion);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(ChoreCompletion choreCompletion)
        {

            choreCompletion.ModifiedAt = DateTime.Now.ToLocalTime();
            choreCompletion.CompletionDateTime = DateTime.SpecifyKind(choreCompletion.CompletionDateTime, DateTimeKind.Local);
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({choreCompletion.ChoreCompletionId})", choreCompletion);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Update failed ({(int)response.StatusCode}): {response.ReasonPhrase}. {body}");
            }
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
