using Core.PharmacyEntities;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        public IMedicineRepository MedicineRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IInventoryRepository InventoryRepository { get; set; }
        public ILocationRepository LocationRepository { get; set; }
        public IReceiptRepository ReceiptRepository { get; set; }

    }
}
