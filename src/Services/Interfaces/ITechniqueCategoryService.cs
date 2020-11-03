using System.Collections.Generic;
using System.Threading.Tasks;
using Athena.Models.DTOs;
using Athena.Models.Entities;

namespace Athena.Services
{
    public interface ITechniqueCategoryService
    {
        Task<TechniqueCategoryDTO> CreateAsync(TechniqueCategoryDTO model);
        Task<TechniqueCategoryDTO> GetByNameAsync(string name);
        Task<ICollection<TechniqueCategoryDTO>> GetAllAsync();
        Task UpdateAsync(string entityName, TechniqueCategoryDTO model);
        Task DeleteAsync(string entityName);
    }
}