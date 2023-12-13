using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PharmacyEntities
{
    public class Report : BaseEntity
    {
        [Required]
        public string PharmacistId { get; set; }
        public ApplicationUser? Pharmacist { get; set; }
        [Required]
        public int ReceiptId { get; set; }
        public Receipt? Receipt { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
