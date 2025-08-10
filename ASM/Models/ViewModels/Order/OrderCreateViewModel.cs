using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.Order
{
        public class OrderCreateViewModel
        {
            [Required]
            public string DeliveryAddress { get; set; } = string.Empty;
            public string? Note { get; set; }

            [Required]
            [MinLength(1)]
            public List<OrderItemCreateViewModel> Items { get; set; } = new();
        }
}
