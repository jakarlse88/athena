using Athena.Repositories;

namespace Athena.Services
{
    public class TechniqueService : ITechniqueService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TechniqueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}