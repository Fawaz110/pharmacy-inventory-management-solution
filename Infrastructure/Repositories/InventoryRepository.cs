using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        private readonly PharmaDbContext _context;

        public InventoryRepository(PharmaDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Inventory> GetCompanyInventories()
            => _context.Inventories.Where(inv => inv.InventoryType == InventoryType.Company).AsNoTracking().ToList();

        public IEnumerable<Inventory> GetPharmaciesInventories()
            => _context.Inventories.Where(inv => inv.InventoryType == InventoryType.Pharmacy).AsNoTracking().ToList();
         
    }
}
