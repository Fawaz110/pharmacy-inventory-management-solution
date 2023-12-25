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
        int DeleteFromInventory(int id);
        Medicine GetLastAddedMedicine();
        int AddMedicineInLocation(MedicineLocations medicineLocation);
        int UpdateMedicineLocation(MedicineLocations medicineLocation);
        MedicineLocations GetLastInsertedMeidcineLocations();
    }
}
