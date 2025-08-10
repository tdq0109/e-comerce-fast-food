﻿namespace ASM.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public int? ProductID { get; set; }
        public int? ComboID { get; set; }
        public int Quantity { get; set; }

        public User? User { get; set; }
        public Product? Product { get; set; }
        public Combo? Combo {  get; set; }
    }
}
