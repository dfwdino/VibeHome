using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities.Recipes;

namespace VibeHome.API.Controllers
{
    public class RecipeIngredientsController : ODataController
    {
        private readonly IRecipeIngredientService _recipeIngredientService;

        public RecipeIngredientsController(IRecipeIngredientService recipeIngredientService)
        {
            _recipeIngredientService = recipeIngredientService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var items = await _recipeIngredientService.GetAllAsync();
            return Ok(items);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var item = await _recipeIngredientService.GetByIdAsync(key);
            if (item == null) return NotFound();
            return Ok(item);
        }

        public async Task<IActionResult> Post([FromBody] RecipeIngredient recipeIngredient)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _recipeIngredientService.AddAsync(recipeIngredient);
            return Created(recipeIngredient);
        }

        public async Task<IActionResult> Put(int key, [FromBody] RecipeIngredient recipeIngredient)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != recipeIngredient.RecipeIngredientId) return BadRequest();
            var existing = await _recipeIngredientService.GetByIdAsync(key);
            if (existing == null) return NotFound();
            await _recipeIngredientService.UpdateAsync(recipeIngredient);
            return Updated(recipeIngredient);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var item = await _recipeIngredientService.GetByIdAsync(key);
            if (item == null) return NotFound();
            await _recipeIngredientService.DeleteAsync(key);
            return NoContent();
        }
    }
}
