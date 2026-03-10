using System.Net.Http.Json;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities.Recipes;

namespace VibeHome.Infrastructure.HttpServices.Recipes
{
    public class UnitTypeHttpService : IUnitTypeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _endpoint = "odata/UnitTypes";

        public UnitTypeHttpService(HttpClient httpClient) { _httpClient = httpClient; }

        public async Task<IEnumerable<UnitType>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<UnitType>>($"{_endpoint}?$orderby=UnitName asc"); 
            return response?.Value ?? Enumerable.Empty<UnitType>();
        }

        public async Task<UnitType?> GetByIdAsync(int id)
        {
            try { return await _httpClient.GetFromJsonAsync<UnitType>($"{_endpoint}({id})"); }
            catch (HttpRequestException) { return null; }
        }

        public async Task AddAsync(UnitType unitType)
        {
            unitType.CreatedAt = DateTime.Now;
            unitType.ModifiedAt = DateTime.Now;
            var response = await _httpClient.PostAsJsonAsync(_endpoint, unitType);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UnitType unitType)
        {
            unitType.ModifiedAt = DateTime.Now;
            unitType.CreatedAt = DateTime.Now;
            var response = await _httpClient.PutAsJsonAsync($"{_endpoint}({unitType.UnitTypeId})", unitType);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_endpoint}({id})");
            response.EnsureSuccessStatusCode();
        }

        private class ODataResponse<T> { public List<T> Value { get; set; } = new(); }
    }
}
