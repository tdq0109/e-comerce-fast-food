namespace ASM.Models.ViewModels.Dashboard
{
    public class DashboardViewModel
    {
        public int TotalOrdersDelivered { get; set; }
        public decimal RevenueToday { get; set; }
        public decimal RevenueThisMonth { get; set; }
        public List<SalesItemViewModel> BestSellingItems { get; set; } = new List<SalesItemViewModel>();
    }
}
