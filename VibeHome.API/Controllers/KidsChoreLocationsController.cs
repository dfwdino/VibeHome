using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class KidsChoreLocationsController : ODataController
    {
        private readonly IKidsChoreLocationService _locationService;

        public KidsChoreLocationsController(IKidsChoreLocationService locationService)
        {
            _locationService = locationService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var locations = await _locationService.GetAllAsync();
            return Ok(locations);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var location = await _locationService.GetByIdAsync(key);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        public async Task<IActionResult> Post([FromBody] KidsChoreLocation location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _locationService.AddAsync(location);
            return Created(location);
        }

        public async Task<IActionResult> Put(int key, [FromBody] KidsChoreLocation location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != location.LocationId)
            {
                return BadRequest();
            }

            var existing = await _locationService.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

            await _locationService.UpdateAsync(location);
            return Updated(location);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var location = await _locationService.GetByIdAsync(key);
            if (location == null)
            {
                return NotFound();
            }

            await _locationService.DeleteAsync(key);
            return NoContent();
        }
    }
}
