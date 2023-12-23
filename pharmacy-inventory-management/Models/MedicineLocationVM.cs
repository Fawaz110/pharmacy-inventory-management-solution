using Core.PharmacyEntities;
using System.ComponentModel.DataAnnotations;

namespace pharmacy_inventory_management.Models
{
    public class MedicineLocationVM
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public int LocationId { get; set; }
        [Required]
        public int MedicineId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public Location? Location { get; set; }
        public Medicine? Medicine { get; set; }
    }
}
