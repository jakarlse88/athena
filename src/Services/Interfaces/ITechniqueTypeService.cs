using System.Collections.Generic;
using System.Threading.Tasks;
using Athena.Models.DTOs;

namespace Athena.Services
{
    public interface ITechniqueTypeService
    {
        Task<TechniqueTypeDTO> CreateAsync(TechniqueTypeDTO model);
        Task<TechniqueTypeDTO> GetByNameAsync(string name);
        Task<ICollection<TechniqueTypeDTO>> GetAllAsync();
        Task DeleteAsync(string entityName);
    }
}