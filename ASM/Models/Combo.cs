namespace ASM.Models
{
    public class Combo
    {
        public int ComboID { get; set; }
        public string ComboName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; }

        public ICollection<ComboItem>? ComboItems { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
