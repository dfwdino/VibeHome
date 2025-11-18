using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class CardioSessionsController : ODataController
    {
        private readonly IRepository<CardioSession> _repository;

        public CardioSessionsController(IRepository<CardioSession> repository)
        {
            _repository = repository;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var sessions = await _repository.GetAllAsync();
            return Ok(sessions);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var session = await _repository.GetByIdAsync(key);
            if (session == null)
            {
                return NotFound();
            }
            return Ok(session);
        }

        public async Task<IActionResult> Post([FromBody] CardioSession session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(session);
            return Created(session);
        }

        public async Task<IActionResult> Put(int key, [FromBody] CardioSession session)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != session.Id)
            {
                return BadRequest();
            }

            var existing = await _repository.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(session);
            return Updated(session);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var session = await _repository.GetByIdAsync(key);
            if (session == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(key);
            return NoContent();
        }
    }
}
