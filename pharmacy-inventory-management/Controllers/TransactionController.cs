using Core.PharmacyEntities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pharmacy_inventory_management.Models;

namespace pharmacy_inventory_management.Controllers
{
    [Authorize(Roles = "Admin, Pharmacist")]
    public class TransactionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Contracted(int? id, string searchValue = "")
        {
            // public string? PharmacyName { get; set; }
            // public int? NumberOfBraches { get; set; }
            // public int? TotalItemsStored { get; set; }

            Dictionary<Inventory?, ContractedVM?> keyValuePairs = new Dictionary<Inventory?, ContractedVM?>();

            var contractedPharmacies = _unitOfWork.InventoryRepository.GetPharmaciesInventories().Distinct();

            if (!string.IsNullOrEmpty(searchValue))
            {
                contractedPharmacies = contractedPharmacies.Where(ph => ph.Name.Trim().ToLower()
                                                                        .Contains(searchValue.Trim().ToLower()));
            }

            foreach (var pharmacy in contractedPharmacies)
            {
                var branches = _unitOfWork.LocationRepository.GetLocationsByInventoryId(pharmacy.Id);

                int numberOfBraches = branches.Count();

                int numberOfItems = 0;

                foreach (var branche in branches)
                {
                    int numberOfItemsInBranche = _unitOfWork.MedicineRepository.GetMedicinesByLocationId(branche.Id).Sum(ml => ml.Quantity);
                    
                    numberOfItems += numberOfItemsInBranche;
                }

                keyValuePairs.Add(pharmacy, 
                    new ContractedVM { 
                        NumberOfBraches = numberOfBraches, 
                        TotalItemsStored = numberOfItems 
                    });
            }

            ViewData["tableOfContracted"] = keyValuePairs;

            ViewBag.id = id;
            return View();
        }

        public IActionResult Invoice(int? id, DateTime? dateForSearch, string? pharmacyNameSearchTerm = "")
        {
            if (pharmacyNameSearchTerm is null)
                pharmacyNameSearchTerm = "";
            // dateForSearch = DateTime.Now;
            IEnumerable<Receipt>? invoices;
            if (dateForSearch is null)
            {
                invoices = _unitOfWork.ReceiptRepository.GetAllInvoices()
                                      .Where(r => r.Receiver.Inventory.Name.Trim().Contains(pharmacyNameSearchTerm.Trim().ToLower()));
            }
            else
            {
                invoices = _unitOfWork.ReceiptRepository.GetAllInvoices()
                                      .Where(r => r.Receiver.Inventory.Name.Trim().Contains(pharmacyNameSearchTerm.Trim().ToLower())
                                                  && r.Date.Day == dateForSearch.Value.Day 
                                                  && r.Date.Month == dateForSearch.Value.Month
                                                  && r.Date.Year == dateForSearch.Value.Year);
            }

            ViewData["invoices"] = invoices;

            return View();
        }

        public IActionResult Returns(int? id, string? pharmacyNameSearchTerm, DateTime? dateForSearch)
        {
            if (pharmacyNameSearchTerm is null)
                pharmacyNameSearchTerm = "";
            // dateForSearch = DateTime.Now;
            IEnumerable<Receipt>? returns;
            if (dateForSearch is null)
            {
                returns = _unitOfWork.ReceiptRepository.GetAllInvoices()
                                      .Where(r => r.Receiver.Inventory.Name.Trim().Contains(pharmacyNameSearchTerm.Trim().ToLower()));
            }
            else
            {
                returns = _unitOfWork.ReceiptRepository.GetAllInvoices()
                                      .Where(r => r.Receiver.Inventory.Name.Trim().Contains(pharmacyNameSearchTerm.Trim().ToLower())
                                                  && r.Date.Day == dateForSearch.Value.Day
                                                  && r.Date.Month == dateForSearch.Value.Month
                                                  && r.Date.Year == dateForSearch.Value.Year);
            }

            ViewData["returns"] = returns;
            return View();
        }
    }
}
