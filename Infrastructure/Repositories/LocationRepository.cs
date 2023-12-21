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
    public class LocationRepository : GenericRepository<Inventory>, ILocationRepository
    {
        private readonly PharmaDbContext _context;

        public LocationRepository(PharmaDbContext context) : base(context)
        {
            _context = context;
        }

        public int Add(Location entity)
        {
            _context.Add(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<Location> GetLocationsByInventoryId(int id)
            => _context.Locations.Where(location => location.Id == id).AsNoTracking().ToList();

        public int Update(Location entity)
        {
            _context.Set<Location>().Update(entity);
            return _context.SaveChanges();
        }

        IEnumerable<Location> IGenericRepository<Location>.GetAll()
            => _context.Locations.AsNoTracking().ToList();

        Location IGenericRepository<Location>.GetById(int id)
            => _context.Locations.AsNoTracking().FirstOrDefault(location => location.Id == id);
    }
}
