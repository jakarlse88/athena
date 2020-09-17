using System.Threading.Tasks;
using Athena.ViewModels;

namespace Athena.Services
{
    public interface ITechniqueService
    {
        Task<TechniqueViewModel> CreateAsync(TechniqueViewModel model);
        Task<TechniqueViewModel> GetByNameAsync(string name);
    }
}