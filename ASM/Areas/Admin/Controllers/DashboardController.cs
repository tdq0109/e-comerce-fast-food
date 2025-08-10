using ASM.Models;
using ASM.Models.ViewModels.Dashboard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var totalOrdersDelivered = await _context.Orders
                .CountAsync(o => o.Status == "Delivered");

            var today = DateTime.Today;
            var revenueToday = await _context.Orders
                .Where(o => o.Status == "Delivered" && o.OrderDate.Date == today)
                .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddTicks(-1);

            var revenueThisMonth = await _context.Orders
                .Where(o => o.Status == "Delivered"
                            && o.OrderDate >= firstDayOfMonth
                            && o.OrderDate <= lastDayOfMonth)
                .SumAsync(o => (decimal?)o.TotalAmount) ?? 0;

            var productSales = await _context.OrderDetails
                .Where(od => od.ProductID != null)
                .GroupBy(od => od.ProductID)
                .Select(g => new
                {
                    ProductID = g.Key,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .ToListAsync();

            var productSalesList = new List<SalesItemViewModel>();
            foreach (var ps in productSales)
            {
                var product = await _context.Products.FindAsync(ps.ProductID);
                if (product != null)
                {
                    productSalesList.Add(new SalesItemViewModel
                    {
                        Name = product.ProductName,
                        Quantity = ps.TotalQuantity,
                        IsCombo = false
                    });
                }
            }

            var comboSales = await _context.OrderDetails
                .Where(od => od.ComboID != null)
                .GroupBy(od => od.ComboID)
                .Select(g => new
                {
                    ComboID = g.Key,
                    TotalQuantity = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .ToListAsync();

            var comboSalesList = new List<SalesItemViewModel>();
            foreach (var cs in comboSales)
            {
                var combo = await _context.Combos.FindAsync(cs.ComboID);
                if (combo != null)
                {
                    comboSalesList.Add(new SalesItemViewModel
                    {
                        Name = combo.ComboName,
                        Quantity = cs.TotalQuantity,
                        IsCombo = true
                    });
                }
            }

            var combinedList = productSalesList.Concat(comboSalesList)
                                               .OrderByDescending(x => x.Quantity)
                                               .ToList();

            var vm = new DashboardViewModel
            {
                TotalOrdersDelivered = totalOrdersDelivered,
                RevenueToday = revenueToday,
                RevenueThisMonth = revenueThisMonth,
                BestSellingItems = combinedList
            };

            return View(vm);
        }
    }
}
