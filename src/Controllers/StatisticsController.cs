using Microsoft.AspNetCore.Mvc;
using Settings;
using University;

namespace Statistics
{
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController(
        StatisticsService statisticsService,
        SettingsService settingsService,
        UniversityProxy universityProxy
        ) : ControllerBase
    {
        private readonly StatisticsService _statisticsService = statisticsService;
        private readonly SettingsService _settingsService = settingsService;
        private readonly UniversityProxy _universityProxy = universityProxy;

        [HttpGet]
        public async Task<IActionResult> GetStatistics()
        {
            var myData = await _settingsService.LoadAsync();
            var universityData = await _universityProxy.GetAbiturients();
            var statistics = _statisticsService.GetStatistics(myData.Uid, universityData);
            return Ok(statistics);
        }
    }
}