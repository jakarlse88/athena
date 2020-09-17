using System.Threading.Tasks;
using Athena.ViewModels;

namespace Athena.Services
{
    public interface ITechniqueTypeService
    {
        Task<TechniqueTypeViewModel> CreateAsync(TechniqueTypeViewModel model);
    }
}