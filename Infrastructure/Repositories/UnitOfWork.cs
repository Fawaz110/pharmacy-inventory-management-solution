using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using System.Collections;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(IMedicineRepository medicineRepository, IUserRepository userRepository)
        {
            MedicineRepository = medicineRepository;
            UserRepository = userRepository;

        }

        public IMedicineRepository MedicineRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
    }
}
