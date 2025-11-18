using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class WorkoutTypesController : ODataController
    {
        private readonly IRepository<WorkoutType> _repository;

        public WorkoutTypesController(IRepository<WorkoutType> repository)
        {
            _repository = repository;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var workoutTypes = await _repository.GetAllAsync();
            return Ok(workoutTypes);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var workoutType = await _repository.GetByIdAsync(key);
            if (workoutType == null)
            {
                return NotFound();
            }
            return Ok(workoutType);
        }

        public async Task<IActionResult> Post([FromBody] WorkoutType workoutType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(workoutType);
            return Created(workoutType);
        }

        public async Task<IActionResult> Put(int key, [FromBody] WorkoutType workoutType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != workoutType.Id)
            {
                return BadRequest();
            }

            var existing = await _repository.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(workoutType);
            return Updated(workoutType);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var workoutType = await _repository.GetByIdAsync(key);
            if (workoutType == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(key);
            return NoContent();
        }
    }
}
