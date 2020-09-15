using Athena.Models;

namespace Athena.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntityBase
    {
        void Add(TEntity entity);
    }
}