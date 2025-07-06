namespace ASM.Models.ViewModels.Feedback
{
    public class FeedbackViewModel
    {
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

}
