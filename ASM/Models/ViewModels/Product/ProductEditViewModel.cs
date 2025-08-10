using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.Product
{
    public class ProductEditViewModel
    {
        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(200, ErrorMessage = "Tên sản phẩm không quá 200 ký tự")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mô tả không được để trống")]
        [StringLength(1000, ErrorMessage = "Mô tả không quá 1000 ký tự")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Giá sản phẩm là bắt buộc")]
        [Range(0.01, 1000000, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }
        public int Quantity { get; set; } 
        public string? ImageURL { get; set; } = string.Empty;

        // Dùng để upload file ảnh
        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "Danh mục là bắt buộc")]
        public int CategoryID { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string? Tags { get; set; }

    }
}
