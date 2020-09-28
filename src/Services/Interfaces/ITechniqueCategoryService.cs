using System.Collections.Generic;
using System.Threading.Tasks;
using Athena.Models.ViewModels;

namespace Athena.Services
{
    public interface ITechniqueCategoryService
    {
        Task<TechniqueCategoryViewModel> CreateAsync(TechniqueCategoryViewModel model);
        Task<TechniqueCategoryViewModel> GetByNameAsync(string name);
        Task<ICollection<TechniqueCategoryViewModel>> GetAllAsync();
    }
}