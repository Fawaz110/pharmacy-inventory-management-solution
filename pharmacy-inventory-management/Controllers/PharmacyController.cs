﻿using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    public class PharmacyController : Controller
    {
        public IActionResult Inventory(string location = "")
        {
            ViewBag.Location = location;
            return View();
        }
    }
}