using System.Reflection;
using System.Threading.Tasks;
using Athena.Models.NewEntities;
using Athena.Repositories;

namespace Athena.Services
{
    public class TechniqueTypeService :  ITechniqueTypeService
    {
        private readonly IRepository<TechniqueType> _techniqueTypeRepository;

        public TechniqueTypeService(IRepository<TechniqueType> techniqueTypeRepository)
        {
            _techniqueTypeRepository = techniqueTypeRepository;
        }

        /// <summary>
        /// Creates a new <see cref="Technique"/> entity.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TechniqueType> CreateAsync(TechniqueTypeViewModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}