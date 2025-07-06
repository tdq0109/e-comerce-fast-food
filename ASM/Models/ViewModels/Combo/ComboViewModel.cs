namespace ASM.Models.ViewModels.Combo
{
    public class ComboViewModel
    {
        public int ComboID { get; set; }
        public string ComboName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
    }
}
