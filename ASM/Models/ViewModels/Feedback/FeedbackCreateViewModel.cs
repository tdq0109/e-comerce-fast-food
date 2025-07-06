using System.ComponentModel.DataAnnotations;

namespace ASM.Models.ViewModels.Feedback
{
    public class FeedbackCreateViewModel
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(500)]
        public string Comment { get; set; } = string.Empty;
    }
}
