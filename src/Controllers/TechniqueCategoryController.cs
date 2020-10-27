using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Athena.Infrastructure.Auth;
using Athena.Models.Entities;
using Athena.Models.Validators;
using Athena.Models.ViewModels;
using Athena.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Athena.Controllers
{
    [ApiController]
    [Route("api/technique/category")]
    public class TechniqueCategoryController : ControllerBase
    {
        private readonly ITechniqueCategoryService _techniqueCategoryService;

        public TechniqueCategoryController(ITechniqueCategoryService techniqueCategoryService)
        {
            _techniqueCategoryService = techniqueCategoryService;
        }

        /// <summary>
        /// Get all <see cref="TechniqueCategory"/> entities,
        /// represented as <see cref="TechniqueCategoryViewModel"/> DTOs.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all/")]
        [Authorize(Policy = AuthorizationPolicyConstants.HasTechniqueCategoryReadPermission)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _techniqueCategoryService.GetAllAsync());
        }
        
        /// <summary>
        /// Gets a <see cref="TechniqueCategory"/> entity by its Name property.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="200">Entity was found.</response>
        /// <response code="400">Bad Name.</response>
        /// <response code="401">User is not authorized to access this resource.</response>
        /// <response code="403">User does not hold sufficient permissions to access this resource.</response>
        /// <response code="404">No entity was found matching the given identifier.</response>
        [HttpGet]
        [Authorize(Policy = AuthorizationPolicyConstants.HasTechniqueCategoryReadPermission)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name) ||
                !new Regex(ValidationRegex.ValidAlphabetic).IsMatch(name)) 
            {
                return BadRequest();
            }

            var result = await _techniqueCategoryService.GetByNameAsync(name);

            return result == null
                ? (IActionResult) NotFound($"Couldn't find any {nameof(TechniqueCategory)} entity matching the name '{name}'.")
                : Ok(result);
        }

        /// <summary>
        /// Creates a new <see cref="TechniqueCategory"/> entity.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Entity was successfully created.</response>
        /// <response code="400">Received a null value for <param name="model"></param>.</response>
        /// <response code="401">User is not authorized to access this resource.</response>
        /// <response code="403">User does not hold sufficient permissions to access this resource.</response>
        [HttpPost]
        [Authorize(Policy = AuthorizationPolicyConstants.HasTechniqueCategoryWritePermission)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
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