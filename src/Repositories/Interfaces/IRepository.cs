using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Athena.Models;
using Athena.Models.NewEntities;

namespace Athena.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntityBase
    {
        void Add(TEntity entity);
        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(int id);
    }
}