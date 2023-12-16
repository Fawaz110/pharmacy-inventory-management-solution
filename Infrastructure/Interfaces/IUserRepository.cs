using Core.PharmacyEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        void AddTrackingRecord(WorkflowTracking workflowTracking);
        IEnumerable<WorkflowTracking> GetWorkflowRecords();
    }
}
