using ASM.Models.ViewModels.Product;

namespace ASM.Models.ViewModels.Combo
{
    public class ComboDetailViewModel : ComboViewModel
    {
        public List<ProductViewModel> Items { get; set; } = new();
    }
}
