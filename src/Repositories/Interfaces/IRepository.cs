using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Athena.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Inserts a new <typeparam name="TEntity"></typeparam> entity into the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> Insert(TEntity entity);
        
        /// <summary>
        /// Persists any changes made to a given <typeparam name="TEntity"></typeparam> entity to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity);
        
        /// <summary>
        /// Deletes a given <typeparam name="TEntity"></typeparam> from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity);
        
        /// <summary>
        /// Gets the subset of all <typeparam name="TEntity"></typeparam> entities that satisfy a given condition.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate);
    }
}