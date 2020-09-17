using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Athena.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Athena.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new ()
    {
        private readonly AthenaDbContext _context;

        public Repository(AthenaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Begins tracking a TEntity entity of type in the 'Added' state, such that it
        /// will be persisted to the DB when SaveChanges() is called.
        /// </summary>
        /// <param name="entity"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<TEntity> Insert(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _context.Add(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception e)
            {
                Log.Error($"An error occurred while attempting to insert a new {typeof(TEntity)} entity: {@e}");
                throw;
            }
        }

        /// <summary>
        /// Gets the subset of all TEntity entities that satisfy a given condition.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));    
            }

            var result = 
                await _context
                    .Set<TEntity>()
                    .Where(predicate)
                    .ToArrayAsync();

            return result;
        }
    }
}