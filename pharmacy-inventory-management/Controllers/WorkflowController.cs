﻿using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace pharmacy_inventory_management.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WorkflowController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkflowController(IUnitOfWork unitOfWork) : base(unitOfWork)
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
