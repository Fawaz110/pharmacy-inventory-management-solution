using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pharmacy_inventory_management.Helper;
using pharmacy_inventory_management.Models;

namespace pharmacy_inventory_management.Controllers
{
    [Authorize(Roles = "Admin, Pharmacist")]
    public class CompanyController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PharmaDbContext _context;

        public CompanyController(IUnitOfWork unitOfWork, PharmaDbContext context) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }
        public IActionResult Inventory(int? id)
        {

            IEnumerable<IGrouping<int, MedicineLocations?>> medicineLocations;

            //Dictionary<int, List<MedicineLocations?>> keyValuePairs = new Dictionary<int, List<MedicineLocations?>>();
            // List<MedicineLocations?> medicines = new List<MedicineLocations?>();

            if (id is not null)
            {
                var inventory = _unitOfWork.InventoryRepository.GetById((int)id);
                ViewData["inventory"] = inventory;

                medicineLocations = _unitOfWork.MedicineRepository.GetAllForComany()
                                                          .Where(ml => ml.Location.InventoryId == id)
                                                          .GroupBy(ml => ml.LocationId);
                

                // get locations of the inventory (using in add proccess)
                // var locationsOfInventory = _unitOfWork.LocationRepository.GetLocationsByInventoryId((int)id);

            }
            else
            {
                medicineLocations = _unitOfWork.MedicineRepository.GetAllForComany().GroupBy(ml => ml.LocationId);

                
                    
                

                //// get all locations of company inventories
                //foreach (var group in medicineLocations)
                //{
                //    var i = group.Key.Inventory;
                //    foreach (var item in group)
                //        if(item.Location.Inventory.InventoryType == InventoryType.Company)
                //        {
                //           var location = _unitOfWork.LocationRepository.GetLocationsByInventoryId(item.Location.InventoryId);
                //        }
                //}
                // var allCompanyLocations = _unitOfWork.LocationRepository.GetLocationsByInventoryId(/* variable */)
            }
            
            //foreach (var group in medicineLocations)
            //{
            //    var location = _unitOfWork.LocationRepository.GetById(group.Key);
            //    foreach (var item in group)
            //    {
            //        if (keyValuePairs.ContainsKey(group.Key))
            //            keyValuePairs[group.Key].Add(item);
            //        else
            //            keyValuePairs.Add(group.Key, new List<MedicineLocations>() { item });
            //    }

            //}

            List<Location> locations = new List<Location>();

            foreach (var group in medicineLocations)
                locations.Add(_unitOfWork.LocationRepository.GetLocationWithInclude(group.Key));

            ViewData["lcoations"] = locations;
            
            ViewData["medicineGroups"] = medicineLocations;
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
        
        public IActionResult UpdateAmount(int medicineId, int locationId)
        {
            var medicineLocation = _unitOfWork.MedicineRepository.GetMedicinesByLocationId(locationId)
                                                                 .Where(ml => ml.MedicineId == medicineId)
                                                                 .FirstOrDefault();

            MedicineLocationVM medicineLocationVM = new MedicineLocationVM
            {
                Id = medicineLocation.Id,
                LocationId = locationId,
                Location = medicineLocation.Location,
                Medicine = medicineLocation.Medicine,
                MedicineId = medicineLocation.MedicineId,
                Quantity = medicineLocation.Quantity
            };
            // ViewData["medicineLocation"] = medicineLocation;

            return View(medicineLocationVM);
        }
        [HttpPost]
        public IActionResult UpdateAmount(MedicineLocationVM medicineLocation)
        {
            if (ModelState.IsValid)
            {
                MedicineLocations updated = new MedicineLocations
                {
                    Id = medicineLocation.Id,
                    LocationId = medicineLocation.LocationId,
                    MedicineId = medicineLocation.MedicineId,
                    Quantity = medicineLocation.Quantity
                };

                var elementToUpdate = _unitOfWork.MedicineRepository.GetMedicinesByLocationId(medicineLocation.LocationId)
                    .Where(ml => ml.MedicineId == medicineLocation.MedicineId).FirstOrDefault();

                _context.Attach(elementToUpdate);
                elementToUpdate.Quantity = medicineLocation.Quantity;

                var updateResult = _unitOfWork.MedicineRepository.UpdateAmount(elementToUpdate);
                return RedirectToAction(nameof(Inventory));
            }
            return View(medicineLocation);
        }
        // [HttpPost]
        public IActionResult DeleteFromInventory(int rowId)
        {
            _unitOfWork.MedicineRepository.DeleteFromInventory(rowId);

            return RedirectToAction(nameof(Inventory));
        }
    }
}
