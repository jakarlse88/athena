using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Athena.Data;
using Athena.Models;
using Microsoft.EntityFrameworkCore;

namespace Athena.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntityBase
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
        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            
            _context.Set<TEntity>().Add(entity);
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

        /// <summary>
        /// Gets a TEntity entity by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<TEntity> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            var entity = 
                await _context
                    .Set<TEntity>()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

            return entity;
        }
    }
}