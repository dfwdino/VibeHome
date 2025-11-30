using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.Infrastructure.HttpServices
{
    public class CardioTypeHttpService : ICardioTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/CardioTypes";

        public CardioTypeHttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CardioType>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<CardioType>>(_endpoint);
            return response?.Value ?? Enumerable.Empty<CardioType>();
        }

        public async Task<CardioType?> GetByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CardioType>($"{_endpoint}({id})");
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public async Task AddAsync(CardioType type)
        {
            var response = await _httpClient.PostAsJsonAsync(_endpoint, type);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(CardioType type)
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
