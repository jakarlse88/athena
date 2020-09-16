using System;
using System.Linq;
using System.Threading.Tasks;
using Athena.Models.NewEntities;
using Athena.Repositories;
using Athena.ViewModels;
using AutoMapper;
using Serilog;

namespace Athena.Services
{
    public class TechniqueService : ITechniqueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TechniqueService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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

            // how do we handle this?
            if (!await NoDuplicateTechniqueName(model.Name))
            {
                try
                {
                    var entity = await MapNewEntity(model);

                    _unitOfWork.TechniqueRepository.Add(entity);

                    await _unitOfWork.CommitAsync();

                    return _mapper.Map<TechniqueViewModel>(entity);
                }
                catch (Exception e)
                {
                    await _unitOfWork.RollbackAsync();

                    Log.Error($"An error occurred while attempting to insert a {nameof(Technique)} entity: {@e}", e);
                }
            }

            return null;
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
                TechniqueCategory = await GetTechniqueCategory(model.TechniqueCategoryId),
                TechniqueType = await GetTechniqueType(model.TechniqueTypeId),
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
        /// <exception cref="Exception"></exception>
        private async Task<TechniqueType> GetTechniqueType(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            
            var techniqueType =
                await _unitOfWork.TechniqueTypeRepository.GetByIdAsync(id);

            if (techniqueType == null)
            {
                throw new Exception(
                    $"An error occurred in {nameof(TechniqueService)}: no {typeof(TechniqueType)} entity with the ID <{id}>.");
            }

            return techniqueType;
        }

        /// <summary>
        /// Gets a <see cref="TechniqueCategory"/> entity by its ID.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private async Task<TechniqueCategory> GetTechniqueCategory(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }
            
            var techniqueCategory =
                await _unitOfWork.TechniqueCategoryRepository.GetByIdAsync(id);

            if (techniqueCategory == null)
            {
                throw new Exception(
                    $"An error occurred in {nameof(TechniqueService)}: no {typeof(TechniqueCategory)} entity with the ID <{id}>.");
            }

            return techniqueCategory;
        }

        /// <summary>
        /// Determines whether there already exists an entity with a given name. 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private async Task<bool> NoDuplicateTechniqueName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            var results = await _unitOfWork.TechniqueRepository.GetByConditionAsync(t =>
                t.Name.ToLower() == name.ToLower());

            return results.Any();
        }
    }
}