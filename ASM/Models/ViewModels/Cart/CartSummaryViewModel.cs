namespace ASM.Models.ViewModels.Cart
{
    public class CartSummaryViewModel
    {
        public List<CartItemViewModel> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }
}
