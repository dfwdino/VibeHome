using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities.Recipes;

namespace VibeHome.API.Controllers
{
    public class RecipeFavoritesController : ODataController
    {
        private readonly IRecipeFavoriteService _recipeFavoriteService;

        public RecipeFavoritesController(IRecipeFavoriteService recipeFavoriteService)
        {
            _recipeFavoriteService = recipeFavoriteService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var items = await _recipeFavoriteService.GetAllAsync();
            return Ok(items);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var item = await _recipeFavoriteService.GetByIdAsync(key);
            if (item == null) return NotFound();
            return Ok(item);
        }

        public async Task<IActionResult> Post([FromBody] RecipeFavorite recipeFavorite)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _recipeFavoriteService.AddAsync(recipeFavorite);
            return Created(recipeFavorite);
        }

        public async Task<IActionResult> Put(int key, [FromBody] RecipeFavorite recipeFavorite)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != recipeFavorite.RecipeFavoriteId) return BadRequest();
            var existing = await _recipeFavoriteService.GetByIdAsync(key);
            if (existing == null) return NotFound();
            await _recipeFavoriteService.UpdateAsync(recipeFavorite);
            return Updated(recipeFavorite);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var item = await _recipeFavoriteService.GetByIdAsync(key);
            if (item == null) return NotFound();
            await _recipeFavoriteService.DeleteAsync(key);
            return NoContent();
        }
    }
}
