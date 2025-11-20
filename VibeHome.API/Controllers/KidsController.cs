using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class KidsController : ODataController
    {
        private readonly IKidService _kidService;

        public KidsController(IKidService kidService)
        {
            _kidService = kidService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var kids = await _kidService.GetAllAsync();
            return Ok(kids);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var kid = await _kidService.GetByIdAsync(key);
            if (kid == null)
            {
                return NotFound();
            }
            return Ok(kid);
        }

        public async Task<IActionResult> Post([FromBody] Kid kid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _kidService.AddAsync(kid);
            return Created(kid);
        }

        public async Task<IActionResult> Put(int key, [FromBody] Kid kid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != kid.KidId)
            {
                return BadRequest();
            }

            var existing = await _kidService.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

            await _kidService.UpdateAsync(kid);
            return Updated(kid);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var kid = await _kidService.GetByIdAsync(key);
            if (kid == null)
            {
                return NotFound();
            }

            await _kidService.DeleteAsync(key);
            return NoContent();
        }
    }
}
