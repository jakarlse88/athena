using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Athena.Models.Entities;
using Athena.Models.ViewModels;
using Athena.Repositories;
using AutoMapper;
using System.Linq;
using System.Text.RegularExpressions;

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
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TechniqueCategoryViewModel> CreateAsync(TechniqueCategoryViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            var entity = new TechniqueCategory { Name = model.Name };

            await _techniqueCategoryRepository.Insert(entity);

            return _mapper.Map<TechniqueCategoryViewModel>(entity);
        }

        /// <summary>
        /// Get a <see cref="TechniqueCategory"/> entity by its Name property.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> argument contains one or more illegal characters.</exception>
        public async Task<TechniqueCategoryViewModel> GetByNameAsync(string name)
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
                await _techniqueCategoryRepository
                    .GetByConditionAsync(t => t.Name.ToLower() == name.ToLower());

            var techniqueCategory = result as TechniqueCategory[] ?? result.ToArray();
            
            return techniqueCategory.Any() 
                ? _mapper.Map<TechniqueCategoryViewModel>(techniqueCategory.FirstOrDefault()) 
                : null;
        }

        /// <summary>
        /// Get all <see cref="TechniqueCategory"/> entities, represented as a collection of
        /// <see cref="TechniqueCategoryViewModel"/> DTOs.
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<TechniqueCategoryViewModel>> GetAll()
        {
            var results = await _techniqueCategoryRepository.GetByConditionAsync(_ => true);

            var models = _mapper.Map<ICollection<TechniqueCategoryViewModel>>(results);

            return models;
        }
    }
}