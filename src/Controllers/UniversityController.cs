using Abiturients;
using Microsoft.AspNetCore.Mvc;
using Settings;

namespace University
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityController(UniversityProxy universityProxy, SettingsService settingsService, DistributionService distributionService) : ControllerBase
    {
        private readonly UniversityProxy _universityProxy = universityProxy;
        private readonly SettingsService _settingsService = settingsService;
        private readonly DistributionService _distributionService = distributionService;

        [HttpGet("majors")]
        public async Task<IActionResult> GetMajors()
        {
            var majors = await _universityProxy.GetMajors();
            return Ok(majors);
        }

        [HttpGet("rating")]
        public async Task<IActionResult> GetRating()
        {
            var universityData = await _universityProxy.GetAbiturients();
            var myData = await _settingsService.LoadAsync();

            var rating = _distributionService.GetRating(universityData, myData.Uid);

            return Ok(rating);
        }
    }
}