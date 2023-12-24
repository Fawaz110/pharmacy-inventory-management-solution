using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PharmacyEntities
{
    public class MedicineReceipt : BaseEntity
    {
        public int MedicineId { get; set; }
        public Medicine? Medicine { get; set; }
        public int ReceiptId { get; set;}
        public Receipt? Receipt { get; set; }
    }
}
