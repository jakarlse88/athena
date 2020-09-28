using System.Collections.Generic;
using System.Threading.Tasks;
using Athena.Models.ViewModels;

namespace Athena.Services
{
    public interface ITechniqueService
    {
        Task<TechniqueViewModel> CreateAsync(TechniqueViewModel model);
        Task<TechniqueViewModel> GetByNameAsync(string name);
        Task<ICollection<TechniqueViewModel>> GetAll();
    }
}