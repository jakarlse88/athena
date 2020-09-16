using System.Threading.Tasks;
using Athena.Models.NewEntities;

namespace Athena.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Technique> TechniqueRepository { get; }

        Task CommitAsync();
        Task RollbackAsync();
    }
}