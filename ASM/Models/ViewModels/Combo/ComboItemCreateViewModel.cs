using ASM.Models.ViewModels.Product;
using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.Combo
{
    public class ComboItemCreateViewModel
    {
        public List<ProductViewModel> Items { get; set; } = new();
    }
}
