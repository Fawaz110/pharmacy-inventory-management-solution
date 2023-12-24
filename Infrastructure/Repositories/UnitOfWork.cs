using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using System.Collections;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(
            IMedicineRepository medicineRepository,
            IUserRepository userRepository,
            ILocationRepository locationRepository,
            IInventoryRepository inventoryRepository,
            IReceiptRepository receiptRepository)
        {
            MedicineRepository = medicineRepository;
            UserRepository = userRepository;
            LocationRepository = locationRepository;
            InventoryRepository = inventoryRepository;
            ReceiptRepository = receiptRepository;
        }

        public IMedicineRepository MedicineRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
        public IInventoryRepository InventoryRepository { get; set; }
        public ILocationRepository LocationRepository { get; set; }
        public IReceiptRepository ReceiptRepository { get; set; }
    }
}
