namespace ASM.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
        public string DeliveryAddress { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string? CancelReason { get; set; }
        public string? Note { get; set; }

        public User? User { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public DeliveryRequest? DeliveryRequest { get; set; }
    }

}
