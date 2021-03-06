﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Athena.Infrastructure.Exceptions;
using Athena.Models.DTOs;
using Athena.Models.Entities;
using Athena.Models.Validators;
using Athena.Repositories;
using AutoMapper;
using Serilog;

namespace Athena.Services
{
    public class TechniqueTypeService : ITechniqueTypeService
    {
        private readonly IRepository<TechniqueType> _techniqueTypeRepository;
        private readonly IMapper _mapper;

        public TechniqueTypeService(IRepository<TechniqueType> techniqueTypeRepository, IMapper mapper)
        {
            _techniqueTypeRepository = techniqueTypeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get a <see cref="TechniqueType"/> entity by its Name property.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> argument contains one or more illegal characters.</exception>
        public async Task<TechniqueTypeDTO> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!new Regex(ValidationRegex.ValidAlphabetic).IsMatch(name))
            {
                throw new ArgumentException("Argument contains one or more invalid characters.", nameof(name));
            }

            var result =
                await _techniqueTypeRepository
                    .GetByConditionAsync(t => t.Name.ToLower() == name.ToLower());

            var techniqueType = result as List<TechniqueType> ?? result.ToList();

            return (techniqueType.Count is 1)
                ? _mapper.Map<TechniqueTypeDTO>(techniqueType.FirstOrDefault())
                : null;
        }

        /// <summary>
        /// Get all <see ctechref="TechniqueType"/> entities.
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<TechniqueTypeDTO>> GetAllAsync()
        {
            var results = await _techniqueTypeRepository.GetByConditionAsync(_ => true);

            return _mapper.Map<ICollection<TechniqueTypeDTO>>(results);
        }

        /// <summary>
        /// Updates an existing <see cref="TechniqueType"/> entity.
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(string entityName, TechniqueTypeDTO model)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                throw new ArgumentNullException(nameof(entityName));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            
            var entity =
                await GetTechniqueTypeAsync(entityName);
            
            MapExistingEntity(entity, model);
            await _techniqueTypeRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Deletes an existing <see cref="TechniqueType"/> entity.
        /// </summary>
        /// <param name="entityName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteAsync(string entityName)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                throw new ArgumentNullException(nameof(entityName));
            }

            try
            {
                await _techniqueTypeRepository.DeleteAsync(await GetTechniqueTypeAsync(entityName));
            }
            catch (EntityNotFoundException e)
            {
                Log.Error($"An error occurred while attempting to delete a '{typeof(TechniqueType)}' entity: {@e}");
                throw;
            }
        }

        /// <summary>
        /// Create a new <see cref="Technique"/> entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TechniqueTypeDTO> CreateAsync(TechniqueTypeDTO model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            var entity = await _techniqueTypeRepository.Insert(new TechniqueType { Name = model.Name });

            return _mapper.Map<TechniqueTypeDTO>(entity);
        }
        
        /**
       *
       * Private helper methods
       * 
       */
        /// <summary>
        /// Gets a <see cref="TechniqueType"/> entity by its 'Name' property.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        private async Task<TechniqueType> GetTechniqueTypeAsync(string entityName)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                throw new ArgumentNullException(nameof(entityName));
            }

            var result =
                await _techniqueTypeRepository.GetByConditionAsync(t => t.Name.ToLower() == entityName.ToLower());

            if (result is null || !result.Any())
            {
                throw new EntityNotFoundException("Couldn't find an entity matching the specified name",
                    nameof(TechniqueType), entityName);
            }

            return result.FirstOrDefault();
        }
        
        /// <summary>
        /// Map the properties of a <see cref="TechniqueTypeDTO"/> model onto an existing
        /// <see cref="TechniqueType"/> entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        private void MapExistingEntity(TechniqueType entity, TechniqueTypeDTO model)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            entity.NameHangeul = model.NameHangeul;
            entity.NameHanja = model.NameHanja;
            entity.Description = model.Description;
        }
    }
}