using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Athena.Models.Entities;
using Athena.Models.ViewModels;
using Athena.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Athena.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechniqueCategoryController : ControllerBase
    {
        private readonly ITechniqueCategoryService _techniqueCategoryService;

        public TechniqueCategoryController(ITechniqueCategoryService techniqueCategoryService)
        {
            _techniqueCategoryService = techniqueCategoryService;
        }

        /// <summary>
        /// Gets a <see cref="TechniqueCategory"/> entity by its Name property.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="200">Entity was found.</response>
        /// <response code="400">Bad Name.</response>
        /// <response code="401">User not authorized.</response>
        /// <response code="404">Entity was not found.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                !new Regex(@"^[a-zA-Z ]*$").IsMatch(name)) // Alphabetical/whitespace only
            {
                return BadRequest();
            }

            var result = await _techniqueCategoryService.GetByNameAsync(name);

            return result == null
                ? (IActionResult) NotFound()
                : Ok(result);
        }

        /// <summary>
        /// Creates a new <see cref="TechniqueCategory"/> entity.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Entity was successfully created.</response>
        /// <response code="400">Received a null value for <param name="model"></param>.</response>
        /// <response code="401">User not authorized.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public async Task<IActionResult> Post(TechniqueCategoryViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var result = await _techniqueCategoryService.CreateAsync(model);

            return CreatedAtAction("Get", new { name = result.Name }, result);
        }
    }
}