using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class IngredientsController : ODataController
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var ingredients = await _ingredientService.GetAllAsync();
            return Ok(ingredients);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var ingredient = await _ingredientService.GetByIdAsync(key);
            if (ingredient == null) return NotFound();
            return Ok(ingredient);
        }

        public async Task<IActionResult> Post([FromBody] Ingredient ingredient)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _ingredientService.AddAndGetAsync(ingredient);
            return Created(created);
        }

        public async Task<IActionResult> Put(int key, [FromBody] Ingredient ingredient)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != ingredient.IngredientId) return BadRequest();
            var existing = await _ingredientService.GetByIdAsync(key);
            if (existing == null) return NotFound();
            await _ingredientService.UpdateAsync(ingredient);
            return Updated(ingredient);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var ingredient = await _ingredientService.GetByIdAsync(key);
            if (ingredient == null) return NotFound();
            await _ingredientService.DeleteAsync(key);
            return NoContent();
        }
    }
}
