using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.Order
{
    public class CancelOrderViewModel
    {
        public int OrderID { get; set; }

        [Required(ErrorMessage = "Lý do hủy đơn là bắt buộc")]
        public string CancelReason { get; set; } = string.Empty;
    }
}
