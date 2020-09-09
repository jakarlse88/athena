using Microsoft.AspNetCore.Mvc;

namespace Athena.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello, world!");
        }
    }
}