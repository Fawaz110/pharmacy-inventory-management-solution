using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PharmacyEntities
{
    public class Location : BaseEntity
    {
        [Required]
        public int InventoryId { get; set; }
        public Inventory? Inventory { get; set; }
        [Required]
        public string Address { get; set; }
        public IEnumerable<MedicineLocations>? MedicineLocations { get; set; }
    }
}
