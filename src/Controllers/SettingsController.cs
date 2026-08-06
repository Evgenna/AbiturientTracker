using Microsoft.AspNetCore.Mvc;

namespace Settings
{
    [ApiController]
    [Route("[controller]")]
    public class SettingsController(SettingsService settingsService) : ControllerBase
    {
        private readonly SettingsService _settingsService = settingsService;

        [HttpGet]
        public async Task<IActionResult> GetSettings()
        {
            var configuration = await _settingsService.LoadAsync();
            return Ok(configuration);
        }

        [HttpPut]
        public async Task<IActionResult> SaveSettings([FromBody] SettingsConfiguration settingsConfiguration)
        {
            await _settingsService.SaveAsync(settingsConfiguration);
            return Ok(settingsConfiguration);
        }
    }
}