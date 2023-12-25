using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WorkflowController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkflowController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Tracking(DateTime? dateForSearch, string? userNameSearchTerm = "")
        {
            if (userNameSearchTerm == null)
                userNameSearchTerm = "";
            IEnumerable<WorkflowTracking> records;
            if(dateForSearch == null)
                records = _unitOfWork.UserRepository.GetWorkflowRecords()
                            .Where(r => r.AdminName.Trim().ToLower().Contains(userNameSearchTerm.Trim().ToLower()));
            else
                records = _unitOfWork.UserRepository.GetWorkflowRecords()
                            .Where(r => r.AdminName.Trim().ToLower().Contains(userNameSearchTerm.Trim().ToLower())
                                     && r.Date.Day == dateForSearch.Value.Day
                                     && r.Date.Month == dateForSearch.Value.Month
                                     && r.Date.Year == dateForSearch.Value.Year);

            
            return View(records);
        }
    }
}
