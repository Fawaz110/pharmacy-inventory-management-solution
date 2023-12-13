using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    public class WorkflowController : Controller
    {
        public IActionResult Tracking()
        {
            return View();
        }
    }
}
