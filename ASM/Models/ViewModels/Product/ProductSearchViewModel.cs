namespace ASM.Models.ViewModels.Product
{
    public class ProductSearchViewModel
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Category { get; set; }
        public string? Keyword { get; set; }
        public string? Topic { get; set; }
    }
}
