using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Athena.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> Insert(TEntity entity);
        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate);
    }
}