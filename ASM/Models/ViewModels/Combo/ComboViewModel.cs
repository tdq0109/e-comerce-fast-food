using ASM.Repository.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASM.Models.ViewModels.Combo
{
    public class ComboViewModel
    {
        public int ComboID { get; set; }
        public string ComboName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile? Image { get; set; }
    }
}
