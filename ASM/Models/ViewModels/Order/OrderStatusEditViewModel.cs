using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.Order
{
    public class OrderStatusEditViewModel
    {
        public int OrderID { get; set; }

        public string CurrentStatus { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn trạng thái đơn hàng")]
        public string NewStatus { get; set; } = string.Empty;

        public List<SelectListItem> StatusOptions { get; set; } = new()
    {
        new SelectListItem { Value = "Pending", Text = "Chưa giao" },
        new SelectListItem { Value = "Shipping", Text = "Đang giao" },
        new SelectListItem { Value = "Delivered", Text = "Đã giao" },
        new SelectListItem { Value = "Canceled", Text = "Đã hủy" }
    };
    }

}
