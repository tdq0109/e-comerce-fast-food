using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
