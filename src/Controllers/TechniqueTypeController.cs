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
    [Route("api/technique/type")]
    public class TechniqueTypeController : ControllerBase
    {
        private readonly ITechniqueTypeService _techniqueTypeService;

        public TechniqueTypeController(ITechniqueTypeService techniqueTypeService)
        {
            _techniqueTypeService = techniqueTypeService;
        }
        
        /// <summary>
        /// Get all <see cref="TechniqueType"/> entities,
        /// represented as <see cref="TechniqueTypeViewModel"/> DTOs.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all/")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var models = await _techniqueTypeService.GetAllAsync();

            return Ok(models);
        }

        /// <summary>
        /// Gets a <see cref="TechniqueType"/> entity by its Name property.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="200">Entity was found.</response>
        /// <response code="400">Bad Name.</response>
        /// <response code="401">User is not authorized to access this resource.</response>
        /// <response code="403">User does not hold sufficient permissions to access this resource.</response>
        /// <response code="404">No entity was found matching the given identifier.</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || 
                !new Regex(@"^[a-zA-Z ]*$").IsMatch(name)) // Alphabetical/whitespace only
            {
                return BadRequest();
            }

            var result = await _techniqueTypeService.GetByNameAsync(name);

            return result == null
                ? (IActionResult) NotFound($"Couldn't find any {nameof(TechniqueType)} entity matching the name '{name}'.")
                : Ok(result);
        }
        
        /// <summary>
        /// Creates a new <see cref="TechniqueType"/> entity.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Entity was successfully created.</response>
        /// <response code="400">Received a null value for <param name="model"></param>.</response>
        /// <response code="401">User is not authorized to access this resource.</response>
        /// <response code="403">User does not hold sufficient permissions to access this resource.</response>
        [HttpPost]
        [Authorize(Policy = "HasTechniqueTypePermissions")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Post(TechniqueTypeViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var result = await _techniqueTypeService.CreateAsync(model);

            return CreatedAtAction("Get", new { name = result.Name }, result);
        }
    }
}