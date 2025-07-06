namespace ASM.Models.ViewModels.Product
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
    }
}
