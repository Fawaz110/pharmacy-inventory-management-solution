using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Inventory(string location = "")
        {
            ViewBag.Location = location;
            return View();
        }
    }
}
