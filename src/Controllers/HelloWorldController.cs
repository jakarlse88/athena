using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Athena.Controllers
{
    [Route("api/v1/[controller]/")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet("unprotected/")]
        [AllowAnonymous]
        public IActionResult GetAnonymous()
        {
            return Ok("Hello, world!");
        }

        [HttpGet("protected/")]
        [Authorize]
        public IActionResult GetAuthorized()
        {
            return Ok("Hello, protected world!");
        }
    }
}