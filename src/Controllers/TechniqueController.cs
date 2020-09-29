﻿using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Athena.Models.Entities;
using Athena.Models.ViewModels;
using Athena.Services;
using AutoMapper;
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
        /// Get a <see cref="Technique"/> entity (represented, if found, as a <see cref="TechniqueViewModel"/> DTO)
        /// by its Name property.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <response code="200">Entity was found.</response>
        /// <response code="400">Bad Name.</response>
        /// <response code="401">User is not authorized to access this resource.</response>
        /// <response code="403">User does not hold sufficient permissions to access this resource.</response>
        /// <response code="404">No entity was found matching the given identifier.</response>
        [HttpGet]
        [Authorize]
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

            var result = await _techniqueService.GetByNameAsync(name);

            return result == null
                ? (IActionResult) NotFound($"Couldn't find any {nameof(Technique)} entity matching the name '{name}'.")
                : Ok(result);
        }

        /// <summary>
        /// Get all <see cref="Technique"/> entities, represented as <see cref="TechniqueViewModel"/> DTOs.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all/")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var models = await _techniqueService.GetAllAsync();

            return Ok(models);
        }

        /// <summary>
        /// Create a new <see cref="Technique"/> entity.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Entity was successfully created.</response>
        /// <response code="400">Received a null value for <param name="model"></param>.</response>
        /// <response code="401">User is not authorized to access this resource.</response>
        /// <response code="403">User does not hold sufficient permissions to access this resource.</response>
        [HttpPost]
        [Authorize(Policy = "HasTechniquePermissions")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Post(TechniqueViewModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            var result = await _techniqueService.CreateAsync(model);

            return CreatedAtAction("Get", new { name = result.Name }, result);
        }

        /// <summary>
        /// Update an existing <see cref="Technique"/> entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <response code="204">Entity was successfully updated.</response>
        /// <response code="401">User is not authorized to access this resource.</response>
        /// <response code="403">User does not hold sufficient permissions to access this resource.</response>
        /// <response code="404">No entity was found matching the given identifier.</response>
        [HttpPut]
        [Authorize(Policy = "HasTechniquePermissions")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(TechniqueViewModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest();
            }

            try
            {
                await _techniqueService.UpdateAsync(model.Name, model);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound($"Couldn't find a {nameof(Technique)} entity matching the name '{model.Name}'.");
            }
        }
    }
}