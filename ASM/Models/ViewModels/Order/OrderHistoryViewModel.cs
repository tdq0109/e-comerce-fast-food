using ASM.Models.ViewModels.Product;

namespace ASM.Models.ViewModels.Order
{
    public class OrderHistoryViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public List<ProductViewModel> Items { get; set; } = new();
    }
}
