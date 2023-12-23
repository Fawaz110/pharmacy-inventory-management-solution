using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MedicineRepository : GenericRepository<Medicine>, IMedicineRepository
    {
        private readonly PharmaDbContext _context;

        public MedicineRepository(PharmaDbContext context) : base(context)
        {
            _context = context;
        }


        public IEnumerable<MedicineLocations> GetMedicinesByLocationId(int id)
            => _context.MedicineLocations.AsNoTracking()
                                         .Where(ml => ml.LocationId == id)
                                         .Include(ml => ml.Medicine)
                                         .Include(ml => ml.Location);

        IEnumerable<MedicineLocations> IMedicineRepository.GetAll()
            => _context.MedicineLocations.AsNoTracking().Include(ml => ml.Location).ThenInclude(loc => loc.Inventory).Include(ml => ml.Medicine);
        public IEnumerable<MedicineLocations> GetAllForComany()
            => _context.MedicineLocations.AsNoTracking().Include(ml => ml.Location).ThenInclude(loc => loc.Inventory)
                .Include(ml => ml.Medicine)
                .Where(ml => ml.Location.Inventory.InventoryType == InventoryType.Company);

        public IEnumerable<MedicineLocations> GetAllForPharmacies()
            => _context.MedicineLocations.AsNoTracking().Include(ml => ml.Location).ThenInclude(loc => loc.Inventory)
                    .Include(ml => ml.Medicine)
                    .Where(ml => ml.Location.Inventory.InventoryType == InventoryType.Company);

        public int UpdateAmount(MedicineLocations entity)
        {
            _context.Update(entity).Property(x => x.Id).IsModified = false;
            return _context.SaveChanges();
        }


    }
}
