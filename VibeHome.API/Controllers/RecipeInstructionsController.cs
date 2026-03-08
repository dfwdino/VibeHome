using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class RecipeInstructionsController : ODataController
    {
        private readonly IRecipeInstructionService _recipeInstructionService;

        public RecipeInstructionsController(IRecipeInstructionService recipeInstructionService)
        {
            _recipeInstructionService = recipeInstructionService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var items = await _recipeInstructionService.GetAllAsync();
            return Ok(items);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var item = await _recipeInstructionService.GetByIdAsync(key);
            if (item == null) return NotFound();
            return Ok(item);
        }

        public async Task<IActionResult> Post([FromBody] RecipeInstruction recipeInstruction)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _recipeInstructionService.AddAsync(recipeInstruction);
            return Created(recipeInstruction);
        }

        public async Task<IActionResult> Put(int key, [FromBody] RecipeInstruction recipeInstruction)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != recipeInstruction.RecipeInstructionId) return BadRequest();
            var existing = await _recipeInstructionService.GetByIdAsync(key);
            if (existing == null) return NotFound();
            await _recipeInstructionService.UpdateAsync(recipeInstruction);
            return Updated(recipeInstruction);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var item = await _recipeInstructionService.GetByIdAsync(key);
            if (item == null) return NotFound();
            await _recipeInstructionService.DeleteAsync(key);
            return NoContent();
        }
    }
}
