﻿using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    public class WorkflowController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkflowController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Tracking()
        {
            var records = _unitOfWork.UserRepository.GetWorkflowRecords();
            return View(records);
        }
    }
}
