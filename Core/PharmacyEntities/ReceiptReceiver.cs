using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.PharmacyEntities
{
    public class ReceiptReceiver
    {
        [Required]
        public int ReceiptId { get; set; }
        public Receipt Receipt { get; set; }
        [Required]
        public int ReceiverId { get; set; }
        public Inventory? Receiver { get; set; }
    }
}
