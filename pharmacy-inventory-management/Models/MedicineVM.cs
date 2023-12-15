using Core.PharmacyEntities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pharmacy_inventory_management.Models
{
    public class MedicineVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        [Column(TypeName = "money")]

        public decimal Price { get; set; }
        [Required]
        public string SideEffects { get; set; }
        [Required]
        public string Indecations { get; set; }
        [Required]
        public DateTime ProductionDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ExpiryDate { get; set; }
        public IFormFile? Image { get; set; }
        [Required]
        public string ImageUrl { get; set; }
    }
}
