using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Athena.Models.Entities;
using Athena.Repositories;
using AutoMapper;
using System.Linq;
using System.Text.RegularExpressions;
using Athena.Infrastructure.Exceptions;
using Athena.Models.DTOs;
using Athena.Models.Validators;
using Serilog;

namespace Athena.Services
{
    public class TechniqueCategoryService : ITechniqueCategoryService
    {
        private readonly IRepository<TechniqueCategory> _techniqueCategoryRepository;
        private readonly IMapper _mapper;

        public TechniqueCategoryService(IRepository<TechniqueCategory> techniqueCategoryRepository, IMapper mapper)
        {
            _techniqueCategoryRepository = techniqueCategoryRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new <see cref="Technique"/> entity.
        /// </summary>
        /// <param entityName="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TechniqueCategoryDTO> CreateAsync(TechniqueCategoryDTO model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            var entity =
                await _techniqueCategoryRepository
                    .Insert(new TechniqueCategory
                    {
                        Name = model.Name,
                        NameHangeul = model.NameHangeul,
                        NameHanja = model.NameHanja,
                        Description = model.Description
                    });

            return _mapper.Map<TechniqueCategoryDTO>(entity);
        }

        /// <summary>
        /// Get a <see cref="TechniqueCategory"/> entity by its Name property.
        /// </summary>
        /// <param entityName="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"><paramref entityName="name"/> argument contains one or more illegal characters.</exception>
        public async Task<TechniqueCategoryDTO> GetByNameAsync(string name)
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
                await _techniqueCategoryRepository
                    .GetByConditionAsync(t => t.Name.ToLower() == name.ToLower());

            var resultList = result as List<TechniqueCategory> ?? result.ToList();

            return resultList.Count > 0
                ? _mapper.Map<TechniqueCategoryDTO>(result.FirstOrDefault())
                : null;
        }

        /// <summary>
        /// Get all <see cref="TechniqueCategory"/> entities, represented as a collection of
        /// <see cref="TechniqueCategoryDTO"/> DTOs.
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<TechniqueCategoryDTO>> GetAllAsync()
        {
            var results = await _techniqueCategoryRepository.GetByConditionAsync(_ => true);

            return results.Any()
                ? _mapper.Map<ICollection<TechniqueCategoryDTO>>(results)
                : new HashSet<TechniqueCategoryDTO>();
        }

        /// <summary>
        /// Updates an existing <see cref="TechniqueCategory"/> entity.
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(string entityName, TechniqueCategoryDTO model)
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
                await GetTechniqueCategoryAsync(entityName);

            MapExistingEntity(entity, model);
            await _techniqueCategoryRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// Deletes the <see cref="TechniqueCategory"/> entity whose 'Name' property matches the
        /// <paramref name="entityName"/> parameter.
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
                await _techniqueCategoryRepository.DeleteAsync(await GetTechniqueCategoryAsync(entityName));
            }
            catch (EntityNotFoundException e)
            {
                Log.Error($"An error occurred while attempting to delete a '{typeof(TechniqueCategory)}' entity: {@e}");
                throw;
            }
        }

        /**
       *
       * Private helper methods
       * 
       */
        /// <summary>
        /// Gets a <see cref="TechniqueCategory"/> entity by its 'Name' property.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        private async Task<TechniqueCategory> GetTechniqueCategoryAsync(string entityName)
        {
            if (string.IsNullOrWhiteSpace(entityName))
            {
                throw new ArgumentNullException(nameof(entityName));
            }

            var result =
                await _techniqueCategoryRepository.GetByConditionAsync(t => t.Name.ToLower() == entityName.ToLower());

            if (result is null || !result.Any())
            {
                throw new EntityNotFoundException("Couldn't find an entity matching the specified name",
                    nameof(TechniqueCategory), entityName);
            }

            return result.FirstOrDefault();
        }

        /// <summary>
        /// Map the properties of a <see cref="TechniqueCategoryDTO"/> model onto an existing
        /// <see cref="TechniqueCategory"/> entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        private void MapExistingEntity(TechniqueCategory entity, TechniqueCategoryDTO model)
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