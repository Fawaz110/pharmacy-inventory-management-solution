using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    // [Authorize(Roles = "Admin, Pharmacist")]
    public class PharmacyController : Controller
    {
        public IActionResult Inventory(string location = "")
        {
            ViewBag.Location = location;
            return View();
        }
    }
}
