using ASM.Repository.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM.Models.ViewModels.Product
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public string Tags { get; set; } = string.Empty;
        public bool IsCombo { get; set; }
        public bool IsAvailable { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile? Image { get; set; }
    }
}
