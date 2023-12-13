using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    public class NotificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
