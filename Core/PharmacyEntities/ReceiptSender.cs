using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PharmacyEntities
{
    public class ReceiptSender : BaseEntity
    {
        [Required]
        public int ReceiptId { get; set; }
        public Receipt? Receipt { get; set; }
        [Required]
        public int SenderId { get; set; }
        public Inventory? Sender { get; set; }
        
    }
}
