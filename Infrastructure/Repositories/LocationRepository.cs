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
    public class LocationRepository : GenericRepository<Location>, ILocationRepository
    {
        private readonly PharmaDbContext _context;

        public LocationRepository(PharmaDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Location> GetLocationsByInventoryId(int id)
            => _context.Locations.Where(location => location.Id == id).AsNoTracking().ToList();
    }
}
