using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ReceiptRepository : GenericRepository<Receipt>, IReceiptRepository
    {
        private readonly PharmaDbContext _context;

        public ReceiptRepository(PharmaDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Receipt> GetAllInvoices()
            => _context.Receipts.Include(r => r.Sender).ThenInclude(l => l.Inventory)
                                .Include(r => r.Receiver).ThenInclude(l => l.Inventory)
                                .Include(r => r.MedicineReceipt).ThenInclude(mr => mr.Medicine)
                                .Where(r => r.ReceiptType == ReceiptType.Invoice).ToList();

        public IEnumerable<Receipt> GetAllReturns()
            => _context.Receipts.Include(r => r.Sender).ThenInclude(l => l.Inventory)
                                .Include(r => r.Receiver).ThenInclude(l => l.Inventory)
                                .Include(r => r.MedicineReceipt).ThenInclude(mr => mr.Medicine)
                                .Where(r => r.ReceiptType == ReceiptType.Return).ToList();

    }
}
