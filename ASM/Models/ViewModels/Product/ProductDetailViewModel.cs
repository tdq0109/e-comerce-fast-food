using ASM.Models.ViewModels.Feedback;

namespace ASM.Models.ViewModels.Product
{
    public class ProductDetailViewModel : ProductViewModel
    {
        public string Description { get; set; } = string.Empty;
        public string? Topic { get; set; }
        public string? Tags { get; set; }
        public List<FeedbackViewModel>? Feedbacks { get; set; }
    }
}
