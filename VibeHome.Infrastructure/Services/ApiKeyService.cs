using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;
using VibeHome.Infrastructure.Data;

namespace VibeHome.Infrastructure.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly VibeHomeDbContext _context;
        private readonly IRepository<ApiKey> _repo;

        public ApiKeyService(VibeHomeDbContext context, IRepository<ApiKey> repo)
        {
            _context = context;
            _repo = repo;
        }

        public async Task<bool> ValidateApiKeyAsync(string apiKey)
        {
            if (string.IsNullOrWhiteSpace(apiKey))
                return false;

            var hashedKey = HashApiKey(apiKey);

            var key = await _context.ApiKeys
                .Where(k => k.KeyValue == hashedKey
                    && k.IsActive
                    && !k.IsDeleted
                    && (k.ExpiresAt == null || k.ExpiresAt > DateTime.UtcNow))
                .FirstOrDefaultAsync();

            if (key != null)
            {
                // Update last used timestamp
                await UpdateLastUsedAsync(key.ApiKeyId);
                return true;
            }

            return false;
        }

        public async Task<ApiKey?> GetByKeyValueAsync(string keyValue)
        {
            var hashedKey = HashApiKey(keyValue);
            return await _context.ApiKeys
                .Where(k => k.KeyValue == hashedKey && !k.IsDeleted)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateLastUsedAsync(int apiKeyId)
        {
            var key = await _repo.GetByIdAsync(apiKeyId);
            if (key != null)
            {
                key.LastUsedAt = DateTime.UtcNow;
                key.ModifiedAt = DateTime.UtcNow;
                await _repo.UpdateAsync(key);
            }
        }

        public async Task<string> GenerateApiKeyAsync(string keyName, string? description = null, DateTime? expiresAt = null)
        {
            // Generate a random API key
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            var apiKey = Convert.ToBase64String(randomBytes);

            // Hash the API key for storage
            var hashedKey = HashApiKey(apiKey);

            var apiKeyEntity = new ApiKey
            {
                KeyName = keyName,
                KeyValue = hashedKey,
                Description = description,
                IsActive = true,
                ExpiresAt = expiresAt,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow
            };

            await _repo.AddAsync(apiKeyEntity);

            // Return the plain text API key (this is the only time it will be visible)
            return apiKey;
        }

        private string HashApiKey(string apiKey)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(apiKey);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
