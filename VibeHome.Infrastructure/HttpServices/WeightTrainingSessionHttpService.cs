using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class WeightTrainingSessionHttpService : IWeightTrainingSessionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/WeightTrainingSessions";

        public WeightTrainingSessionHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WeightTrainingSession>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<WeightTrainingSession>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<WeightTrainingSession>();
        }

        public async Task<WeightTrainingSession?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<WeightTrainingSession>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task AddAsync(WeightTrainingSession session)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, session);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(WeightTrainingSession session)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({session.Id})", session);
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
