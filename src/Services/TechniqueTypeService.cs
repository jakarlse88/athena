using System;
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