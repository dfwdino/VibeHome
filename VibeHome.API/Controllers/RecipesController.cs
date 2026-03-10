using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities.Recipes;

namespace VibeHome.API.Controllers
{
    public class RecipesController : ODataController
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var recipes = await _recipeService.GetAllAsync();
            return Ok(recipes);
        }

        public async Task<IActionResult> Get(int key)
        {
            var recipe = await _recipeService.GetByIdAsync(key);
            if (recipe == null) return NotFound();
            return new JsonResult(recipe); // Bypasses OData formatter, uses System.Text.Json so nav props are included
        }

        public async Task<IActionResult> Post([FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await _recipeService.AddAndGetAsync(recipe);
            return Created(created);
        }

        public async Task<IActionResult> Put(int key, [FromBody] Recipe recipe)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != recipe.RecipeId) return BadRequest();
            var existing = await _recipeService.GetByIdAsync(key);
            if (existing == null) return NotFound();
            await _recipeService.UpdateAsync(recipe);
            return Updated(recipe);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var recipe = await _recipeService.GetByIdAsync(key);
            if (recipe == null) return NotFound();
            await _recipeService.DeleteAsync(key);
            return NoContent();
        }
    }
}
