using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    [Authorize(Roles = "Admin, Pharmacist")]
    public class DashboardController : BaseController
    {
        public DashboardController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
