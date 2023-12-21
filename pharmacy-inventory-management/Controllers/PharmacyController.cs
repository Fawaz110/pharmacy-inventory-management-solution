using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    [Authorize(Roles = "Admin, Pharmacist")]
    public class PharmacyController : BaseController
    {
        public PharmacyController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IActionResult Inventory(string location = "")
        {
            ViewBag.Location = location;
            return View();
        }
    }
}
