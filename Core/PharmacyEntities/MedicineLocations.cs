using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PharmacyEntities
{
    public class MedicineLocations : BaseEntity
    {
        public Medicine? Medicine { get; set; }
        public Location? Location { get; set; }
        public int LocationId { get; set; }
        public int MedicineId { get; set; }
        public int Quantity { get; set; }
    }
}
