using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
                                         .Include(ml => ml.Location).ToList();

        IEnumerable<MedicineLocations> IMedicineRepository.GetAll()
            => _context.MedicineLocations.AsNoTracking().Include(ml => ml.Location).ThenInclude(loc => loc.Inventory).Include(ml => ml.Medicine);
        public IEnumerable<MedicineLocations> GetAllForComany()
            => _context.MedicineLocations.AsNoTracking().Include(ml => ml.Location).ThenInclude(loc => loc.Inventory)
                .Include(ml => ml.Medicine)
                .Where(ml => ml.Location.Inventory.InventoryType == InventoryType.Company);

        public IEnumerable<MedicineLocations> GetAllForPharmacies()
            => _context.MedicineLocations.AsNoTracking().Include(ml => ml.Location).ThenInclude(loc => loc.Inventory)
                    .Include(ml => ml.Medicine)
                    .Where(ml => ml.Location.Inventory.InventoryType == InventoryType.Pharmacy);

        public int UpdateAmount(MedicineLocations entity)
        {
            _context.Update(entity).Property(x => x.Id).IsModified = false;
            return _context.SaveChanges();
        }

        public int DeleteFromInventory(int id)
        {
            _context.Remove(_context.MedicineLocations.FirstOrDefault(ml => ml.Id == id));
            return _context.SaveChanges();
        }

        public Medicine GetLastAddedMedicine()
            => _context.Medicines.OrderBy(m => m.Id).LastOrDefault();

        public int AddMedicineInLocation(MedicineLocations medicineLocation)
        {
            _context.MedicineLocations.Add(medicineLocation);
            return _context.SaveChanges();
        }

        public int UpdateMedicineLocation(MedicineLocations medicineLocation)
        {
            _context.Update(medicineLocation);
            return _context.SaveChanges();
        }

        public MedicineLocations GetLastInsertedMeidcineLocations()
            => _context.MedicineLocations.OrderBy(x => x.Id).LastOrDefault();
    }
}
