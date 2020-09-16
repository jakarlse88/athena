using System.Threading.Tasks;
using Athena.Data;
using Athena.Models.NewEntities;

namespace Athena.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AthenaDbContext _context;
        private IRepository<Technique> _techniqueRepository;

        public UnitOfWork(AthenaDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets, and if necessary instantiates the TechniqueRepository instance.
        /// </summary>
        public IRepository<Technique> TechniqueRepository =>
            _techniqueRepository ??= new Repository<Technique>(_context);
        
        /// <summary>
        /// Asynchronously persists any currently tracked changes to the DB.
        /// </summary>
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        /// <summary>
        ///  Asynchronously Rolls back any currently tracked changes.
        /// </summary>
        public async Task RollbackAsync()
        {
            await _context.DisposeAsync();
        }
    }
}