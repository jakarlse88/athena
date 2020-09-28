using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Athena.Models.Entities;
using Athena.Models.ViewModels;
using Athena.Repositories;
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
        public async Task<TechniqueViewModel> CreateAsync(TechniqueViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            var entity = await MapNewEntity(model);

            await _techniqueRepository.Insert(entity);

            return _mapper.Map<TechniqueViewModel>(entity);
        }

        /// <summary>
        /// Gets a <see cref="Technique"/> entity by its Name property.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// /// <exception cref="ArgumentException"><paramref name="name"/> argument contains one or more illegal characters.</exception>
        public async Task<TechniqueViewModel> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!new Regex(@"^[a-zA-Z ]*$").IsMatch(name))
            {
                throw new ArgumentException("Argument contains one or more invalid characters.", nameof(name));
            }
            
            var result =
                await _techniqueRepository
                    .GetByConditionAsync(t => t.Name.ToLower() == name.ToLower());

            var technique = result as Technique[] ?? result.ToArray();
            
            return technique.Any() 
                ? _mapper.Map<TechniqueViewModel>(technique.FirstOrDefault()) 
                : null;
        }

        /// <summary>
        /// Get all <see ctechref="Technique"/> entities.
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<TechniqueViewModel>> GetAll()
        {
            var results = await _techniqueRepository.GetByConditionAsync(_ => true);

            var models = _mapper.Map<ICollection<TechniqueViewModel>>(results);

            return models;
        }

        /**
         *
         * Private helper methods
         * 
         */
        /// <summary>
        /// Maps a given <see cref="TechniqueViewModel"/> model to a new <seealso cref="Technique"/> entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private async Task<Technique> MapNewEntity(TechniqueViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var entity = new Technique
            {
                TechniqueCategoryNameNavigation = await GetTechniqueCategory(model.TechniqueCategoryName),
                TechniqueTypeNameNavigation = await GetTechniqueType(model.TechniqueTypeName),
                Name = model.Name,
                NameHangeul = model.NameHangeul,
                NameHanja = model.NameHanja
            };

            return entity;
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

            var techniqueType =
                await _techniqueTypeRepository.GetByConditionAsync(x => x.Name.ToLower() == name.ToLower());

            if (!techniqueType.Any())
            {
                throw new Exception(
                    $"An error occurred in {nameof(TechniqueService)}: no {typeof(TechniqueType)} entity with the Name <{name}>.");
            }

            return techniqueType.FirstOrDefault();
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

            var techniqueCategory =
                await _techniqueCategoryRepository.GetByConditionAsync(x => x.Name.ToLower() == name.ToLower());

            if (!techniqueCategory.Any())
            {
                throw new Exception(
                    $"An error occurred in {nameof(TechniqueService)}: no {typeof(TechniqueCategory)} entity with the Name <{name}>.");
            }

            return techniqueCategory.FirstOrDefault();
        }
    }
}