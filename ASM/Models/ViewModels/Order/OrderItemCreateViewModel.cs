using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.Order
{
    public class OrderItemCreateViewModel
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}
