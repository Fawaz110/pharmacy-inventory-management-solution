using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using System.Collections;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        public UnitOfWork(IMedicineRepository medicineRepository)
        {
            MedicineRepository = medicineRepository;
        }

        public IMedicineRepository MedicineRepository { get; set; }
    }
}
