using System.Collections.Generic;
using System.Threading.Tasks;
using Athena.Models.DTOs;

namespace Athena.Services
{
    public interface ITechniqueService
    {
        Task<TechniqueDTO> CreateAsync(TechniqueDTO model);
        Task<TechniqueDTO> GetByNameAsync(string name);
        Task<ICollection<TechniqueDTO>> GetAllAsync();
        Task UpdateAsync(string entityName, TechniqueDTO model);
    }
}