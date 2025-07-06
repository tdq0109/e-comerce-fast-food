namespace ASM.Models
{
    public class DeliveryRequest
    {
        public int RequestID { get; set; }
        public int OrderID { get; set; }
        public DateTime RequestedTime { get; set; } = DateTime.Now;
        public string DeliveryNote { get; set; } = string.Empty;

        public Order? Order { get; set; }
    }

}
