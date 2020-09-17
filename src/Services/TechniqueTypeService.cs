using System;
using System.Linq;
using System.Threading.Tasks;
using Athena.Models.Entities;
using Athena.Repositories;
using Athena.ViewModels;
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
        /// Gets a <see cref="Technique"/> entity by its Name property.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TechniqueTypeViewModel> GetByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            var result =
                await _techniqueTypeRepository
                    .GetByConditionAsync(t => t.Name.ToLower() == name.ToLower());

            return result.Any() 
                ? _mapper.Map<TechniqueTypeViewModel>(result.FirstOrDefault()) 
                : null;
        }

        /// <summary>
        /// Creates a new <see cref="Technique"/> entity.
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