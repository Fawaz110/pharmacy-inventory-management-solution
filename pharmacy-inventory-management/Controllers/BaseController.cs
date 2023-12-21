using Core.PharmacyDbContext;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace pharmacy_inventory_management.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewData["pharmacies"] = _unitOfWork.InventoryRepository.GetPharmaciesInventories();
            ViewData["company"] = _unitOfWork.InventoryRepository.GetCompanyInventories();

            base.OnActionExecuting(context);
        }

        public BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
