namespace ASM.Models.ViewModels.Cart
{
    public class CartItemViewModel
    {
        public int? ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? ComboID { get; set; }
        public string ImageURL { get; set; } = string.Empty;
    }
}
