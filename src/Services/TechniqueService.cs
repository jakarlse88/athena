using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Athena.Infrastructure.Exceptions;
using Athena.Models.DTOs;
using Athena.Models.Entities;
using Athena.Repositories;
using Athena.Models.Validators;
using AutoMapper;

namespace Athena.Services
{
    public class TechniqueService : ITechniqueService
    {
        private readonly IRepository<Technique> _techniqueRepository;
        private readonly IRepository<TechniqueType> _techniqueTypeRepository;
        private readonly IRepository<TechniqueCategory> _techniqueCategoryRepository;
        private readonly IMapper _mapper;

        public TechniqueService(IMapper mapper, IRepository<Technique> techniqueRepository,
            IRepository<TechniqueType> techniqueTypeRepository, IRepository<TechniqueCategory> techniqueCategoryRepository)
        {
            _mapper = mapper;
            _techniqueRepository = techniqueRepository;
            _techniqueTypeRepository = techniqueTypeRepository;
            _techniqueCategoryRepository = techniqueCategoryRepository;
        }

        /// <summary>
        /// Creates a new <see cref="Technique"/> entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TechniqueDTO> CreateAsync(TechniqueDTO model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            var entity = await _techniqueRepository.Insert(await MapNewEntity(model));

            return _mapper.Map<TechniqueDTO>(entity);
        }

        /// <summary>
        /// Gets a <see cref="Technique"/> entity by its Name property.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// /// <exception cref="ArgumentException"><paramref name="name"/> argument contains one or more illegal characters.</exception>
        public async Task<TechniqueDTO> GetByNameAsync(string name)
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
                await _techniqueRepository
                    .GetByConditionAsync(t => t.Name.ToLower() == name.ToLower());

            var technique = result as List<Technique> ?? result.ToList();
            
            return (technique.Count is 1)
                ? _mapper.Map<TechniqueDTO>(technique.FirstOrDefault()) 
                : null;
        }

        /// <summary>
        /// Get all <see ctechref="Technique"/> entities.
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<TechniqueDTO>> GetAllAsync()
        {
            var results = await _techniqueRepository.GetByConditionAsync(_ => true);

            return _mapper.Map<ICollection<TechniqueDTO>>(results);
        }

        /// <summary>
        /// Update a <see cref="Technique"/> entity.
        /// </summary>
        /// <param name="entityName"></param>
        /// <param name="model">Model containing updated properties.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task UpdateAsync(string entityName, TechniqueDTO model)
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
                await GetTechniqueAsync(entityName);

            await MapExistingEntity(entity, model);
            await _techniqueRepository.UpdateAsync(entity);
        }

        /**
         *
         * Private helper methods
         * 
         */
        
        /// <summary>
        /// Gets a <see cref="Technique"/> entity by its 'Name' property.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        private async Task<Technique> GetTechniqueAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            var entity =
                await _techniqueRepository.GetByConditionAsync(t => t.Name.ToLower() == name.ToLower());

            if (entity is null)
            {
                throw new EntityNotFoundException("Couldn't find an entity matching the specified name", nameof(Technique), name);
            }

            return entity.FirstOrDefault();
        }
        
        /// <summary>
        /// Maps a given <see cref="TechniqueDTO"/> model to a new <seealso cref="Technique"/> entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<Technique> MapNewEntity(TechniqueDTO model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return new Technique
            {
                TechniqueCategoryNameNavigation = await GetTechniqueCategory(model.TechniqueCategoryName),
                TechniqueTypeNameNavigation = await GetTechniqueType(model.TechniqueTypeName),
                Name = model.Name,
                NameHangeul = model.NameHangeul,
                NameHanja = model.NameHanja,
                Description = model.Description
            };
        }

        /// <summary>
        /// Map the properties of a <see cref="TechniqueDTO"/> model onto an existing
        /// <see cref="Technique"/> entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="model"></param>
        private async Task MapExistingEntity(Technique entity, TechniqueDTO model)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            
            entity.TechniqueCategoryNameNavigation = await GetTechniqueCategory(model.TechniqueCategoryName);
            entity.TechniqueTypeNameNavigation = await GetTechniqueType(model.TechniqueTypeName);
            entity.NameHangeul = model.NameHangeul;
            entity.NameHanja = model.NameHanja;
            entity.Description = model.Description;
        }

        /// <summary>
        /// Gets a <see cref="TechniqueType"/> entity by its ID.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        private async Task<TechniqueType> GetTechniqueType(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var result =
                await _techniqueTypeRepository.GetByConditionAsync(x => x.Name.ToLower() == name.ToLower());

            var techniqueType = result.FirstOrDefault();
            
            if (techniqueType is null)
            {
                throw new Exception(
                    $"An error occurred in {nameof(TechniqueService)}: no {typeof(TechniqueType)} entity with the Name <{name}>.");
            }

            return techniqueType;
        }

        /// <summary>
        /// Gets a <see cref="TechniqueCategory"/> entity by its ID.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        private async Task<TechniqueCategory> GetTechniqueCategory(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var result =
                await _techniqueCategoryRepository.GetByConditionAsync(x => x.Name.ToLower() == name.ToLower());

            var techniqueCategory = result.FirstOrDefault();
            
            if (techniqueCategory is null)
            {
                throw new Exception(
                    $"An error occurred in {nameof(TechniqueService)}: no {typeof(TechniqueCategory)} entity with the Name <{name}>.");
            }

            return techniqueCategory;
        }
    }
}