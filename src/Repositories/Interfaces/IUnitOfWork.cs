using System.Threading.Tasks;
using Athena.Models.NewEntities;

namespace Athena.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Technique> TechniqueRepository { get; }
        IRepository<TechniqueCategory> TechniqueCategoryRepository { get; }
        IRepository<TechniqueType> TechniqueTypeRepository { get; }

        Task CommitAsync();
        Task RollbackAsync();
    }
}