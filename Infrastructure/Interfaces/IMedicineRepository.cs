using Core.PharmacyEntities;

namespace Infrastructure.Interfaces
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        IEnumerable<MedicineLocations> GetMedicinesByLocationId(int id);
        IEnumerable<MedicineLocations> GetAll();
        IEnumerable<MedicineLocations> GetAllForComany();
        IEnumerable<MedicineLocations> GetAllForPharmacies();
        int UpdateAmount(MedicineLocations entity);
    }
}
