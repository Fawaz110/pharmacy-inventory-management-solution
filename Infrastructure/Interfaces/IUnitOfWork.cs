using Core.PharmacyEntities;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        public IMedicineRepository MedicineRepository { get; set; }
    }
}
