using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class JournalEntriesController : ODataController
    {
        private readonly IJournalEntryService _journalEntryService;

        public JournalEntriesController(IJournalEntryService journalEntryService)
        {
            _journalEntryService = journalEntryService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var journalEntries = await _journalEntryService.GetAllAsync();
            return Ok(journalEntries);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var journalEntry = await _journalEntryService.GetByIdAsync(key);
            if (journalEntry == null)
            {
                return NotFound();
            }
            return Ok(journalEntry);
        }

        public async Task<IActionResult> Post([FromBody] JournalEntry journalEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _journalEntryService.AddAsync(journalEntry);
            return Created(journalEntry);
        }

        public async Task<IActionResult> Put(int key, [FromBody] JournalEntry journalEntry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != journalEntry.Id)
            {
                return BadRequest();
            }

            var existing = await _journalEntryService.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

            await _journalEntryService.UpdateAsync(journalEntry);
            return Updated(journalEntry);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var journalEntry = await _journalEntryService.GetByIdAsync(key);
            if (journalEntry == null)
            {
                return NotFound();
            }

            await _journalEntryService.DeleteAsync(key);
            return NoContent();
        }
    }
}
