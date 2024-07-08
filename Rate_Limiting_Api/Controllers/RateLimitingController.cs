using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Rate_Limiting_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("Temel")]
    public class RateLimitingController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
        [HttpGet("Async")]
        public async Task<IActionResult> GetAsync()
        {
            return Ok();
        }
    }
}
