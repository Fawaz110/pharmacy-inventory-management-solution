using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

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
           => _context.Set<TEntity>().AsNoTracking().FirstOrDefault(element => element.Id == id);

        public IEnumerable<TEntity> GetAll()
            => _context.Set<TEntity>().AsNoTracking().ToList();

        public int Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return _context.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            _context.Update(entity);
            return _context.SaveChanges();
        }

        public int Delete(int id)
        {
            _context.Remove(_context.Medicines.Find(id));
            return _context.SaveChanges();
        }
    }
}
