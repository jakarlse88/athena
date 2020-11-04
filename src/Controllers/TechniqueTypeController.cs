using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Athena.Infrastructure.Auth;
using Athena.Infrastructure.Exceptions;
using Athena.Models.DTOs;
using Athena.Models.Entities;
using Athena.Models.Validators;
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
        /// represented as <see cref="TechniqueTypeDTO"/> DTOs.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all/")]
        [Authorize(Policy = AuthorizationPolicyConstants.TechniqueTypeReadPermission)]
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
        [Authorize(Policy = AuthorizationPolicyConstants.TechniqueTypeReadPermission)]
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
        [Authorize(Policy = AuthorizationPolicyConstants.TechniqueTypeWritePermission)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Post(TechniqueTypeDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var result = await _techniqueTypeService.CreateAsync(model);

            return CreatedAtAction("Get", new { name = result.Name }, result);
        }
        
        /// <summary>
        /// Update an existing <see cref="TechniqueType"/> entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="204">Entity was successfully updated.</response>
        /// <response code="401">User is not authorized to access this resource.</response>
        /// <response code="403">User does not hold sufficient permissions to access this resource.</response>
        /// <response code="404">No entity was found matching the given identifier.</response>
        [HttpPut]
        [Authorize(Policy = AuthorizationPolicyConstants.TechniqueTypeUpdatePermission)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(TechniqueTypeDTO model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest();
            }

            try
            {
                await _techniqueTypeService.UpdateAsync(model.Name, model);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound($"Couldn't find a {nameof(TechniqueType)} entity matching the name '{model.Name}'");
            }
        }
        
        /// <summary>
        /// Deletes an existing <see cref="TechniqueType"/> entity.
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        /// <response code="204">Entity was successfully deleted.</response>
        /// <response code="401">User is not authorized to access this resource.</response>
        /// <response code="403">User does not hold sufficient permissions to access this resource.</response>
        /// <response code="404">No entity was found matching the given identifier.</response>
        [HttpDelete]
        [Authorize(Policy = AuthorizationPolicyConstants.TechniqueTypeDeletePermission)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string entityName)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                return BadRequest();
            }

            try
            {
                await _techniqueTypeService.DeleteAsync(entityName);
                return NoContent();
            }
            catch (EntityNotFoundException)
            {
                return NotFound($"Couldn't find a {nameof(TechniqueType)} entity matching the name '{entityName}'");
            }
        }
    }
}