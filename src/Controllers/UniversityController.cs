using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAbirurients()
        {
            var abiturients = await _universityProxy.GetAbiturients();
            return Ok(abiturients);
        }
    }
}