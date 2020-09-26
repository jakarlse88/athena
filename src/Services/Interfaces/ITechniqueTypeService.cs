using System.Threading.Tasks;
using Athena.Models.ViewModels;

namespace Athena.Services
{
    public interface ITechniqueTypeService
    {
        Task<TechniqueTypeViewModel> CreateAsync(TechniqueTypeViewModel model);
        Task<TechniqueTypeViewModel> GetByNameAsync(string name);
    }
}