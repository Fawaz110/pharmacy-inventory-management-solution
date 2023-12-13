using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    public class TransactionController : Controller
    {
        public IActionResult Contracted(int? id)
        {
            ViewBag.id = id;
            return View();
        }

        public IActionResult Invoice(int? id, string? name, DateTime date)
        {
            return View();
        }

        public IActionResult Returns(int? id, string? name, DateTime date)
        {
            return View();
        }
    }
}
