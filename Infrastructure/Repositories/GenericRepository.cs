using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly PharmaDbContext _context;

        public GenericRepository(PharmaDbContext context)
        {
            _context = context;
        }
        public TEntity GetById(int id)
            => _context.Set<TEntity>().FirstOrDefault(element => element.Id == id);

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
