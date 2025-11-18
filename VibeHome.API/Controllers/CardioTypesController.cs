using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class CardioTypesController : ODataController
    {
        private readonly IRepository<CardioType> _repository;

        public CardioTypesController(IRepository<CardioType> repository)
        {
            _repository = repository;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var cardioTypes = await _repository.GetAllAsync();
            return Ok(cardioTypes);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var cardioType = await _repository.GetByIdAsync(key);
            if (cardioType == null)
            {
                return NotFound();
            }
            return Ok(cardioType);
        }

        public async Task<IActionResult> Post([FromBody] CardioType cardioType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _repository.AddAsync(cardioType);
            return Created(cardioType);
        }

        public async Task<IActionResult> Put(int key, [FromBody] CardioType cardioType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != cardioType.Id)
            {
                return BadRequest();
            }

            var existing = await _repository.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

            await _repository.UpdateAsync(cardioType);
            return Updated(cardioType);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var cardioType = await _repository.GetByIdAsync(key);
            if (cardioType == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(key);
            return NoContent();
        }
    }
}
