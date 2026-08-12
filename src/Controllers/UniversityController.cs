using Abiturients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace University
{
    [ApiController]
    [Route("[controller]")]
    public class UniversityController(UniversityProxy universityProxy) : ControllerBase
    {
        private readonly UniversityProxy _universityProxy = universityProxy;

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
            var abiturients = DistributionService.Prepare(abiturientList);

            var majors = abiturientList.Select(a => a.Major).ToList();

            abiturients = DistributionService.Distribute(abiturients, majors);

            return Ok(abiturients);
        }
    }
}