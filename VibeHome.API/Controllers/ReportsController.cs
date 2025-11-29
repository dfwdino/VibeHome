using Microsoft.AspNetCore.Mvc;
using VibeHome.Application.Interfaces;

namespace VibeHome.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("weekly-earnings")]
        public async Task<IActionResult> GetWeeklyEarnings()
        {
            var result = await _reportService.GetWeeklyEarningsAsync();
            return Ok(result);
        }

        [HttpGet("monthly-earnings")]
        public async Task<IActionResult> GetMonthlyEarnings()
        {
            var result = await _reportService.GetMonthlyEarningsAsync();
            return Ok(result);
        }
    }
}
