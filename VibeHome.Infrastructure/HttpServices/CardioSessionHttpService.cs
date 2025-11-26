using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class CardioSessionHttpService : ICardioSessionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/CardioSessions";

        public CardioSessionHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CardioSession>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<CardioSession>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<CardioSession>();
        }

        public async Task<CardioSession?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CardioSession>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task AddAsync(CardioSession session)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, session);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(CardioSession session)
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
