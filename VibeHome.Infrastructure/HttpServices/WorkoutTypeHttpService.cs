using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class WorkoutTypeHttpService : IWorkoutTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/WorkoutTypes";

        public WorkoutTypeHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<WorkoutType>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<WorkoutType>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<WorkoutType>();
        }

        public async Task<WorkoutType?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<WorkoutType>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task AddAsync(WorkoutType type)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, type);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(WorkoutType type)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({type.Id})", type);
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
