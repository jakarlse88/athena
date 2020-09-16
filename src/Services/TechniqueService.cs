using System;
using System.Threading.Tasks;
using Athena.Repositories;
using Athena.ViewModels;

namespace Athena.Services
{
    public class TechniqueService : ITechniqueService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TechniqueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TechniqueViewModel> CreateAsync(TechniqueViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            
            throw new System.NotImplementedException();
        }
    }
}