using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using pharmacy_inventory_management.Helper;
using pharmacy_inventory_management.Models;
using System.Data;
using System.Reflection.Metadata;

namespace pharmacy_inventory_management.Controllers
{
    [Authorize(Roles = "Admin, Pharmacist")]
    public class CompanyController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Inventory(string location = "")
        {
            // we need to use ViewData to access all inventories to view it in side nav
            ViewData["medicine"] = _unitOfWork.MedicineRepository.GetAll();
            ViewBag.Location = location;
            return View(new MedicineVM());
        }
        [HttpPost]
        public IActionResult Inventory(MedicineVM medicine)
        {
            if (medicine.Image != null && ModelState.ErrorCount == 1)
            {
                medicine.ImageUrl = DocumentSettings.UploadFile(medicine.Image, "Images");
                ModelState.Clear();
            }
            if (ModelState.IsValid)
            {
                var addedMedicine = new Medicine
                {
                    Name = medicine.Name,
                    Category = medicine.Category,
                    ExpiryDate = medicine.ExpiryDate,
                    Indecations = medicine.Indecations,
                    Price = medicine.Price,
                    ProductionDate = medicine.ProductionDate,
                    SideEffects = medicine.SideEffects,
                    ImageUrl = DocumentSettings.UploadFile(medicine.Image, "Images")
                };

                var location = TempData["location"];
                _unitOfWork.MedicineRepository.Add(addedMedicine);
                return RedirectToAction("Inventory");
            }
            ViewData["medicine"] = _unitOfWork.MedicineRepository.GetAll();
            return View(medicine);
        }
        public IActionResult Details(int id)
        {
            var medicine = _unitOfWork.MedicineRepository.GetById(id);
            return View(medicine);
        }
        public IActionResult Update(int id)
        {
            var medicine = _unitOfWork.MedicineRepository.GetById(id);
            var medicineToUpdate = new MedicineVM
            {
                Id = id,
                Name = medicine.Name,
                Category = medicine.Category,
                ImageUrl = medicine.ImageUrl,
                Price = medicine.Price,
                Indecations = medicine.Indecations,
                SideEffects = medicine.SideEffects,
                ExpiryDate = medicine.ExpiryDate,
                ProductionDate = medicine.ProductionDate,

            };
            return View(medicineToUpdate);
        }
        [HttpPost]
        public IActionResult Update(MedicineVM medicine)
        {
            if (medicine.Image == null)
            {
                medicine.ImageUrl = _unitOfWork.MedicineRepository.GetById(medicine.Id).ImageUrl;
            }
            else if(ModelState.ErrorCount == 1)
            {
                medicine.ImageUrl = DocumentSettings.UploadFile(medicine.Image, "Images");
                ModelState.Clear();
            }
            // ImageUrl is null becouse no input in previous view refer to ImageUrl or (Image then resolving it)
            
            if (ModelState.IsValid)
            {
                var updatedMedicine = new Medicine
                {
                    Id = medicine.Id,
                    Name = medicine.Name,
                    Category = medicine.Category,
                    ImageUrl = medicine.ImageUrl,
                    Price = medicine.Price,
                    Indecations = medicine.Indecations,
                    SideEffects = medicine.SideEffects,
                    ExpiryDate = medicine.ExpiryDate,
                    ProductionDate = medicine.ProductionDate
                };

                if(updatedMedicine.ImageUrl == "" || updatedMedicine == null)
                    return View(medicine);

                _unitOfWork.MedicineRepository.Update(updatedMedicine);
                return RedirectToAction(nameof(Details), new { id = updatedMedicine.Id });
            }
            return View(medicine);
        }
        public IActionResult Delete(int id)
        {
            _unitOfWork.MedicineRepository.Delete(id);
            return RedirectToAction("Inventory");
        }
    }
}
