using System;
using System.Threading.Tasks;
using Athena.Models.NewEntities;
using Athena.Services;
using Athena.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Athena.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TechniqueController : ControllerBase
    {
        private readonly ITechniqueService _techniqueService;

        public TechniqueController(ITechniqueService techniqueService)
        {
            _techniqueService = techniqueService;
        }

        /// <summary>
        /// Gets a <see cref="Technique"/> entity by its Name property.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="200">Entity was found.</response>
        /// <response code="400">Bad ID.</response>
        /// <response code="401">User not authorized.</response>
        /// <response code="404">Entity was not found.</response>
        [HttpGet, Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Creates a new <see cref="Technique"/> entity.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Entity was successfully created.</response>
        /// <response code="400">Received a null value for <param name="model"></param>.</response>
        /// <response code="401">User not authorized.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post(TechniqueViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var result = await _techniqueService.CreateAsync(model);

            return CreatedAtAction("Get", new { id = result.Name }, result);
        }
    }
}