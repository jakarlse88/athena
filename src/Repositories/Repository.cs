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
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : class, new ()
    {
        private readonly AthenaDbContext _context;

        public Repository(AthenaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Inserts a new <typeparam name="TEntity"></typeparam> entity into the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
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
        /// Persists any changes made to a given <typeparam name="TEntity"></typeparam> entity to the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error($"An error occurred while attempting to update a {typeof(TEntity)} entity: {@e}");
                throw;
            }
            
        }

        /// <summary>
        /// Deletes a given <typeparam name="TEntity"></typeparam> from the database.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task DeleteAsync(TEntity entity)
        {
            if (entity is null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            try
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Log.Error($"An error occurred while attempting to update a '{typeof(TEntity)}' entity: {@e}");
                throw;
            }
        }

        /// <summary>
        /// Gets the subset of all <typeparam name="TEntity"></typeparam> entities that satisfy a given condition.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
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
                    .ToListAsync();

            return result;
        }
    }
}