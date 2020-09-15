using System;
using Athena.Data;
using Athena.Models;

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
        /// Begins tracking an entity of type <typeparam name="TEntity"></typeparam> in the 'Added' state, such that it
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
    }
}