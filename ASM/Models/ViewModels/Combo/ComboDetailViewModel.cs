using ASM.Models.ViewModels.Product;

namespace ASM.Models.ViewModels.Combo
{
    public class ComboDetailViewModel : ComboViewModel
    {
        public string Description { get; set; } = string.Empty;
        public List<ProductViewModel> Items { get; set; } = new();
    }
}
