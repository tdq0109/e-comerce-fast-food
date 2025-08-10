using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.Order
{
    public class OrderItemCreateViewModel
    {
        [Required]
        public int? ProductID { get; set; }
        public int? ComboID { get; set; }
        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Quantity * Price;
        public string ProductName { get; set; } = string.Empty;
    }
}
