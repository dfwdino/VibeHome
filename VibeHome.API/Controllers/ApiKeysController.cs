using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class ApiKeysController : ODataController
    {
        private readonly IApiKeyService _apiKeyService;
        private readonly IRepository<ApiKey> _repository;

        public ApiKeysController(IApiKeyService apiKeyService, IRepository<ApiKey> repository)
        {
            _apiKeyService = apiKeyService;
            _repository = repository;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var apiKeys = await _repository.GetAllAsync();
            return Ok(apiKeys);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var apiKey = await _repository.GetByIdAsync(key);
            if (apiKey == null)
            {
                return NotFound();
            }
            return Ok(apiKey);
        }

        [HttpPost("odata/ApiKeys/Generate")]
        public async Task<IActionResult> Generate([FromBody] GenerateApiKeyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var apiKey = await _apiKeyService.GenerateApiKeyAsync(
                request.KeyName,
                request.Description,
                request.ExpiresAt);

            return Ok(new
            {
                message = "API Key generated successfully. Please save this key as it will not be shown again.",
                apiKey = apiKey,
                keyName = request.KeyName
            });
        }

        public async Task<IActionResult> Put(int key, [FromBody] ApiKey apiKey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != apiKey.ApiKeyId)
            {
                return BadRequest();
            }

            var existing = await _repository.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

            // Preserve the original KeyValue (don't allow changing the actual key)
            apiKey.KeyValue = existing.KeyValue;
            await _repository.UpdateAsync(apiKey);
            return Updated(apiKey);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var apiKey = await _repository.GetByIdAsync(key);
            if (apiKey == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(key);
            return NoContent();
        }
    }

    public class GenerateApiKeyRequest
    {
        public string KeyName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}
