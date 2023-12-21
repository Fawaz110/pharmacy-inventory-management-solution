using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    [Authorize(Roles = "Admin, Pharmacist")]
    public class TransactionController : BaseController
    {
        public TransactionController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

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
