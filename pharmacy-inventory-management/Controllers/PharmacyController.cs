using Core.PharmacyDbContext;
using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pharmacy_inventory_management.Models;

namespace pharmacy_inventory_management.Controllers
{
    [Authorize(Roles = "Admin, Pharmacist")]
    public class PharmacyController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PharmaDbContext _context;

        public PharmacyController(IUnitOfWork unitOfWork, PharmaDbContext context) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult Inventory(int? id,
                    string MedicineNameSearchTerm = "",
                    string InvLocationSearchTerm = "",
                    string MedicineCategorySearchTerm = "")
        {
            if (MedicineNameSearchTerm is null)
                MedicineNameSearchTerm = "";

            if (InvLocationSearchTerm is null)
                InvLocationSearchTerm = "";

            if (MedicineCategorySearchTerm is null)
                MedicineCategorySearchTerm = "";


            IEnumerable<IGrouping<int, MedicineLocations?>> medicineLocations;

            //Dictionary<int, List<MedicineLocations?>> keyValuePairs = new Dictionary<int, List<MedicineLocations?>>();
            // List<MedicineLocations?> medicines = new List<MedicineLocations?>();

            if (id is not null)
            {
                var inventory = _unitOfWork.InventoryRepository.GetById((int)id);
                ViewData["inventory"] = inventory;

                medicineLocations = _unitOfWork.MedicineRepository.GetAllForPharmacies()
                                            .Where(
                                                ml => ml.Location.InventoryId == id
                                                && ml.Medicine.Name.Trim().ToLower().Contains(MedicineNameSearchTerm.Trim().ToLower())
                                                && ml.Location.Address.Trim().ToLower().Contains(InvLocationSearchTerm.Trim().ToLower())
                                                && ml.Medicine.Category.Trim().ToLower().Contains(MedicineCategorySearchTerm.Trim().ToLower())
                                                ).GroupBy(ml => ml.LocationId);



                // get locations of the inventory (using in add proccess)
                // var locationsOfInventory = _unitOfWork.LocationRepository.GetLocationsByInventoryId((int)id);

            }
            else
            {
                medicineLocations = _unitOfWork.MedicineRepository.GetAllForPharmacies()
                                             .Where(
                                                ml => ml.Medicine.Name.Trim().ToLower().Contains(MedicineNameSearchTerm.Trim().ToLower())
                                                && ml.Location.Address.Trim().ToLower().Contains(InvLocationSearchTerm.Trim().ToLower())
                                                && ml.Medicine.Category.Trim().ToLower().Contains(MedicineCategorySearchTerm.Trim().ToLower())
                                                ).GroupBy(ml => ml.LocationId);

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

        public IActionResult Details(int medicineId, int locationId)
        {
            MedicineLocations medicineLocation;
            if (locationId != 0)
            {
                medicineLocation = _unitOfWork.MedicineRepository.GetMedicinesByLocationId(locationId)
                                                                .Where(ml => ml.MedicineId == medicineId).FirstOrDefault();
            }
            else
            {
                medicineLocation = _unitOfWork.MedicineRepository.GetAllForPharmacies().FirstOrDefault(ml => ml.MedicineId == medicineId);
            }
            // var medicine = _unitOfWork.MedicineRepository.GetById(medicineId);

            ViewBag.Edit = false;
            return View(medicineLocation);
        }
        public IActionResult DeleteFromInventory(int rowId)
        {

            _unitOfWork.MedicineRepository.DeleteFromInventory(rowId);
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
    }

}
