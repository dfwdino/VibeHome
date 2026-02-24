using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Net.Mime;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{

    public class ChoreCompletionsController : ODataController
    {
        private readonly IChoreCompletionService _choreCompletionService;

        public ChoreCompletionsController(IChoreCompletionService choreCompletionService)
        {
            _choreCompletionService = choreCompletionService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var choreCompletions = await _choreCompletionService.GetAllAsync();
            return Ok(choreCompletions);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var choreCompletion = await _choreCompletionService.GetByIdAsync(key);
            if (choreCompletion == null)
            {
                return NotFound();
            }
            return Ok(choreCompletion);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ChoreCompletion choreCompletion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _choreCompletionService.AddAsync(choreCompletion);
            return Created(choreCompletion);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, [FromBody] ChoreCompletion choreCompletion)
        {
            if (choreCompletion == null)
            {
                return BadRequest("Request body is required.");
            }

            // Use key from URL as authority (OData binding can leave key as 0 in some hosts)
            var id = key > 0 ? key : choreCompletion.ChoreCompletionId;
            if (id <= 0)
            {
                return BadRequest("A valid ChoreCompletionId is required (in URL or body).");
            }
            choreCompletion.ChoreCompletionId = id;

            if (!ModelState.IsValid)
            {
                return BadRequest(new { Message = "Validation failed", Errors = ModelState.Where(x => x.Value?.Errors?.Count > 0).ToDictionary(k => k.Key, v => v.Value?.Errors?.Select(e => e.ErrorMessage).ToArray()) });
            }

            var existing = await _choreCompletionService.GetByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            // Preserve server-controlled fields so client doesn't overwrite with defaults
            choreCompletion.CreatedAt = existing.CreatedAt;
            choreCompletion.ModifiedAt = DateTime.UtcNow;
            choreCompletion.IsDeleted = existing.IsDeleted;

            await _choreCompletionService.UpdateAsync(choreCompletion);
            return Updated(choreCompletion);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var choreCompletion = await _choreCompletionService.GetByIdAsync(key);
            if (choreCompletion == null)
            {
                return NotFound();
            }

            await _choreCompletionService.DeleteAsync(key);
            return NoContent();
        }
    }
}
