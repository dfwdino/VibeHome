using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities.Recipes;

namespace VibeHome.API.Controllers
{
    public class UnitTypesController : ODataController
    {
        private readonly IUnitTypeService _unitTypeService;

        public UnitTypesController(IUnitTypeService unitTypeService)
        {
            _unitTypeService = unitTypeService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var unitTypes = await _unitTypeService.GetAllAsync();
            return Ok(unitTypes);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var unitType = await _unitTypeService.GetByIdAsync(key);
            if (unitType == null) return NotFound();
            return Ok(unitType);
        }

        public async Task<IActionResult> Post([FromBody] UnitType unitType)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _unitTypeService.AddAsync(unitType);
            return Created(unitType);
        }

        public async Task<IActionResult> Put(int key, [FromBody] UnitType unitType)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (key != unitType.UnitTypeId) return BadRequest();
            var existing = await _unitTypeService.GetByIdAsync(key);
            if (existing == null) return NotFound();
            await _unitTypeService.UpdateAsync(unitType);
            return Updated(unitType);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var unitType = await _unitTypeService.GetByIdAsync(key);
            if (unitType == null) return NotFound();
            await _unitTypeService.DeleteAsync(key);
            return NoContent();
        }
    }
}
