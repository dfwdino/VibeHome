using VibeHome.Domain.Entities;

namespace VibeHome.Application.Interfaces
{
    public interface IApiKeyService
    {
        Task<bool> ValidateApiKeyAsync(string apiKey);
        Task<ApiKey?> GetByKeyValueAsync(string keyValue);
        Task UpdateLastUsedAsync(int apiKeyId);
        Task<string> GenerateApiKeyAsync(string keyName, string? description = null, DateTime? expiresAt = null);
    }
}
