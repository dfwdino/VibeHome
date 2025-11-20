using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class WorkoutLocationsController : ODataController
    {
        private readonly IRepository<WorkoutLocation> _repository;

        public WorkoutLocationsController(IRepository<WorkoutLocation> repository)
        {
            _repository = repository;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var locations = await _repository.GetAllAsync();
            return Ok(locations);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var location = await _repository.GetByIdAsync(key);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        public async Task<IActionResult> Post([FromBody] WorkoutLocation location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(location);
            return Created(location);
        }

        public async Task<IActionResult> Put(int key, [FromBody] WorkoutLocation location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != location.Id)
            {
                return BadRequest();
            }

            var existing = await _repository.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(location);
            return Updated(location);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var location = await _repository.GetByIdAsync(key);
            if (location == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(key);
            return NoContent();
        }
    }
}
