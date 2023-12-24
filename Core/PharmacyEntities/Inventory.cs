using System.ComponentModel.DataAnnotations;

namespace Core.PharmacyEntities
{
    public enum InventoryType
    {
        Company, Pharmacy
    }
    public class Inventory :BaseEntity
    {
        public string Name { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public InventoryType InventoryType { get; set; }
        public IEnumerable<Location>? CompanyLocations { get; set; }
    }
}
