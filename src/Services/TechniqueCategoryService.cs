using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Athena.Models.Entities;
using Athena.Models.ViewModels;
using Athena.Repositories;
using AutoMapper;
using System.Linq;
using System.Text.RegularExpressions;
using Athena.Models.Validators;

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

            if (!new Regex(ValidationRegex.ValidAlphabetic).IsMatch(name))
            {
                throw new ArgumentException("Argument contains one or more invalid characters.", nameof(name));
            }

            var result =
                await _techniqueCategoryRepository
                    .GetByConditionAsync(t => t.Name.ToLower() == name.ToLower());

            var resultList = result as List<TechniqueCategory> ?? result.ToList();

            return resultList.Count > 0
                ? _mapper.Map<TechniqueCategoryViewModel>(result.FirstOrDefault())
                : null;
        }

        /// <summary>
        /// Get all <see cref="TechniqueCategory"/> entities, represented as a collection of
        /// <see cref="TechniqueCategoryViewModel"/> DTOs.
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<TechniqueCategoryViewModel>> GetAllAsync()
        {
            var results = await _techniqueCategoryRepository.GetByConditionAsync(_ => true);

            return results.Any()
                ? _mapper.Map<ICollection<TechniqueCategoryViewModel>>(results)
                : new HashSet<TechniqueCategoryViewModel>();
        }
    }
}