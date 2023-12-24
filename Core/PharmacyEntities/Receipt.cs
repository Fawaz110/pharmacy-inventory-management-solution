using System.ComponentModel.DataAnnotations;

namespace Core.PharmacyEntities
{
    public enum ReceiptType
    {
        Invoice, Return
    }
    public class Receipt : BaseEntity
    {
        //[Required]
        //public int SenderInventoryId { get; set; }
        //[Required]
        //public int RecieverInventoryId { get; set; }
        //public IEnumerable<Inventory>? ReceiverInventory { get; set; }

        [Required]
        public ReceiptType ReceiptType { get; set; }
        [Required]
        public int TotalAmount { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Location? Sender { get; set; }
        public int SenderId { get; set; }
        public Location? Receiver { get; set; }
        public int ReceiverId { get; set; }
        public IEnumerable<MedicineReceipt>? MedicineReceipt { get; set; }
        //public IEnumerable<ReceiptSender>? ReceiptSender{ get; set; }
        //public IEnumerable<ReceiptReceiver>? ReceiptReceiver { get; set; }
    }
}
