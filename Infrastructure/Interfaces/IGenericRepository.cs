using Core.PharmacyEntities;

namespace Infrastructure.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        int Add(TEntity entity);
        int Update(TEntity entity);
        int Delete(int id);
    }
}
