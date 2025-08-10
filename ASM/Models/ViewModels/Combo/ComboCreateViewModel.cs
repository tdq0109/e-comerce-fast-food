using ASM.Repository.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.Combo
{
    public class ComboCreateViewModel
    {
        [Required(ErrorMessage = "Tên combo là bắt buộc")]
        public string ComboName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập mô tả")]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Giá combo phải lớn hơn hoặc bằng 0")]
        public decimal Price { get; set; }

        public bool IsAvailable { get; set; } = true;

        [NotMapped]
        [FileExtension]
        public IFormFile? Image { get; set; }

        // Danh sách sản phẩm trong combo
        public ComboItemCreateViewModel ItemsCombo { get; set; } = new();
    }

}
