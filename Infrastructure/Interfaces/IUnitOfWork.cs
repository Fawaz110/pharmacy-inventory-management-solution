using Core.PharmacyEntities;

namespace Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        public IMedicineRepository MedicineRepository { get; set; }
        public IUserRepository UserRepository { get; set; }
    }
}
