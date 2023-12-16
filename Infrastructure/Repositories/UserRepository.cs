using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PharmaDbContext _context;

        public UserRepository(PharmaDbContext context)
        {
            _context = context;
        }

        public void AddTrackingRecord(WorkflowTracking workflowTracking)
            => _context.WorkflowTrackingRecords.Add(workflowTracking);
    }
}
