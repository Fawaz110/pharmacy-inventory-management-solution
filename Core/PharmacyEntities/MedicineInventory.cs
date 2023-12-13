using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PharmacyEntities
{
    public class MedicineInventory : BaseEntity
    {
        public Medicine? Medicine { get; set; }
        public Inventory? Inventory { get; set; }
        public int inventoryId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
        
    }
}
