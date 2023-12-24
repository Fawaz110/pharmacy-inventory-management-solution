using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
        [Required]
        public string ImageUrl { get; set; }
        public IEnumerable<MedicineLocations>? MedicineLocations { get; set; }
        public IEnumerable<MedicineReceipt>? MedicineReceipt { get; set; }
    }
}
