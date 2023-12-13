using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
    {

        public MedicineRepository(PharmaDbContext context) : base(context)
        {

        }
    }
}
