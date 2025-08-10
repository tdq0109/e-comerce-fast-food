namespace ASM.Models
{
    public class OrderDetail
    {
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        public int? ProductID { get; set; }
        public int? ComboID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public Order? Order { get; set; }
        public Product? Product { get; set; }
        public Combo? Combo { get; set; }
    }

}
