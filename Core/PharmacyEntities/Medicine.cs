using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PharmacyEntities
{
    public class Medicine : BaseEntity
    {
        [Required]
        public string Name { get; set; }
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
        public IEnumerable<MedicineInventory>? Inventories { get; set; }
    }
}
