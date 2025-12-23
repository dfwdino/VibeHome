using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VibeHome.Application.Interfaces;
using VibeHome.Domain.Entities;

namespace VibeHome.API.Controllers
{
    public class WeeklyPaymentStatusesController : ODataController
    {
        private readonly IWeeklyPaymentStatusService _paymentStatusService;

        public WeeklyPaymentStatusesController(IWeeklyPaymentStatusService paymentStatusService)
        {
            _paymentStatusService = paymentStatusService;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var paymentStatuses = await _paymentStatusService.GetAllAsync();
            return Ok(paymentStatuses);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var paymentStatus = await _paymentStatusService.GetByIdAsync(key);
            if (paymentStatus == null)
            {
                return NotFound();
            }
            return Ok(paymentStatus);
        }

     
        public async Task<IActionResult> Post([FromBody] WeeklyPaymentStatus paymentStatus)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                Console.WriteLine($"[DEBUG POST] ModelState errors: {string.Join(", ", errors.Select(e => e.ErrorMessage))}");

                return BadRequest(ModelState);
            }

            await _paymentStatusService.AddAsync(paymentStatus);
            return Created(paymentStatus);
        }

        public async Task<IActionResult> Put(int key, [FromBody] WeeklyPaymentStatus paymentStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (key != paymentStatus.WeeklyPaymentStatusId)
            {
                return BadRequest();
            }

            var existing = await _paymentStatusService.GetByIdAsync(key);
            if (existing == null)
            {
                return NotFound();
            }

            await _paymentStatusService.UpdateAsync(paymentStatus);
            return Updated(paymentStatus);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var paymentStatus = await _paymentStatusService.GetByIdAsync(key);
            if (paymentStatus == null)
            {
                return NotFound();
            }

            await _paymentStatusService.DeleteAsync(key);
            return NoContent();
        }
    }
}
