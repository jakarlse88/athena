﻿using System;
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
    public class TechniqueTypeService :  ITechniqueTypeService
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
        public async Task<TechniqueTypeViewModel> GetByNameAsync(string name)
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
                await _techniqueTypeRepository
                    .GetByConditionAsync(t => t.Name.ToLower() == name.ToLower());

            var techniqueType = result as TechniqueType[] ?? result.ToArray();
            
            return techniqueType.Any() 
                ? _mapper.Map<TechniqueTypeViewModel>(techniqueType.FirstOrDefault()) 
                : null;
        }

        /// <summary>
        /// Get all <see ctechref="TechniqueType"/> entities.
        /// </summary>
        /// <returns></returns>
        public async Task<ICollection<TechniqueTypeViewModel>> GetAllAsync()
        {
            var results = await _techniqueTypeRepository.GetByConditionAsync(_ => true);

            var models = _mapper.Map<ICollection<TechniqueTypeViewModel>>(results);

            return models;
        }

        /// <summary>
        /// Create a new <see cref="Technique"/> entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TechniqueTypeViewModel> CreateAsync(TechniqueTypeViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                throw new ArgumentNullException(nameof(model.Name));
            }

            var entity = new TechniqueType { Name = model.Name };

            await _techniqueTypeRepository.Insert(entity);

            return _mapper.Map<TechniqueTypeViewModel>(entity);
        }
    }
}