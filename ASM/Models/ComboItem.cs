namespace ASM.Models
{
    public class ComboItem
    {
        public int ComboID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public Combo? Combo { get; set; }
        public Product? Product { get; set; }
    }
}
