using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class ChoreItemsController : ODataController
    {
        private readonly IChoreItemService _choreItemService;

        public ChoreItemsController(IChoreItemService choreItemService)
        {
            _choreItemService = choreItemService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var choreItems = await _choreItemService.GetAllAsync();
            return Ok(choreItems);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var choreItem = await _choreItemService.GetByIdAsync(key);
            if (choreItem == null)
            {
                return NotFound();
            }
            return Ok(choreItem);
        }

        public async Task<IActionResult> Post([FromBody] ChoreItem choreItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _choreItemService.AddAsync(choreItem);
            return Created(choreItem);
        }

        public async Task<IActionResult> Put(int key, [FromBody] ChoreItem choreItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != choreItem.ChoreItemId)
            {
                return BadRequest();
            }

            var existing = await _choreItemService.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

            await _choreItemService.UpdateAsync(choreItem);
            return Updated(choreItem);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var choreItem = await _choreItemService.GetByIdAsync(key);
            if (choreItem == null)
            {
                return NotFound();
            }

            await _choreItemService.DeleteAsync(key);
            return NoContent();
        }
    }
}
