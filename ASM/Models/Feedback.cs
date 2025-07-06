namespace ASM.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string? Type { get; set; } // review, complaint, etc
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public User? User { get; set; }
        public Product? Product { get; set; }
    }

}
