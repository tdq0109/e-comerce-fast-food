namespace ASM.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ImageURL { get; set; } = string.Empty;
        public int CategoryID { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsCombo { get; set; } = false;
        public string? Topic { get; set; }
        public string? Tags { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Category? Category { get; set; }
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Feedback>? Feedbacks { get; set; }
        public ICollection<ComboItem>? ComboItems { get; set; }
    }

}
