using Microsoft.AspNetCore.Mvc;

namespace Statistics
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController(StatisticsService statisticsService) : ControllerBase
    {
        private readonly StatisticsService _statisticsService = statisticsService;

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var statistics = await _statisticsService.GetStatistics();
            return Ok(statistics);
        }
    }
}