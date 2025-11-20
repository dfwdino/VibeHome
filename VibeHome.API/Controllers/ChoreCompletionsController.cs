using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
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

        public async Task<IActionResult> Post([FromBody] ChoreCompletion choreCompletion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _choreCompletionService.AddAsync(choreCompletion);
            return Created(choreCompletion);
        }

        public async Task<IActionResult> Put(int key, [FromBody] ChoreCompletion choreCompletion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != choreCompletion.ChoreCompletionId)
            {
                return BadRequest();
            }

            var existing = await _choreCompletionService.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

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
