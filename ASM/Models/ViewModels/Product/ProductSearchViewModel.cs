namespace ASM.Models.ViewModels.Product
{
    public class ProductSearchViewModel
    {
        public string? ProductName { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? CategoryName { get; set; }
    }
}
