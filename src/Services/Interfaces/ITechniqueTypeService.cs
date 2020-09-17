using System.Threading.Tasks;
using Athena.Models.NewEntities;

namespace Athena.Services
{
    public interface ITechniqueTypeService
    {
        Task<TechniqueType> CreateAsync(TechniqueTypeViewModel model);
    }
}