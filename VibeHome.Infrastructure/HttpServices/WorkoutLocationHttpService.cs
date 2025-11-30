using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class WorkoutLocationHttpService : IWorkoutLocationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/WorkoutLocations";

        public WorkoutLocationHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WorkoutLocation>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<WorkoutLocation>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<WorkoutLocation>();
        }

        public async Task<WorkoutLocation?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<WorkoutLocation>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task AddAsync(WorkoutLocation location)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, location);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(WorkoutLocation location)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({location.Id})", location);
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
