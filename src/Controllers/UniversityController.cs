using Abiturients;
using Microsoft.AspNetCore.Mvc;

namespace University
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityController(UniversityProxy universityProxy, DistributionService distributionService) : ControllerBase
    {
        private readonly UniversityProxy _universityProxy = universityProxy;
        private readonly DistributionService _distributionService = distributionService;

        [HttpGet("majors")]
        public async Task<IActionResult> GetMajors()
        {
            var majors = await _universityProxy.GetMajors();
            return Ok(majors);
        }
        [HttpGet("abiturients")]
        public async Task<IActionResult> GetAbiturients()
        {
            var abiturientList = await _universityProxy.GetAbiturients();
            var abiturients = _distributionService.Prepare(abiturientList);
            return Ok(abiturients);
        }
    }
}