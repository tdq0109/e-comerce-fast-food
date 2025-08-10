using ASM.Models;
using ASM.Models.ViewModels.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ASM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public OrderController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            // Lấy danh sách đơn hàng của user (hoặc tất cả nếu admin)
            var orders = await _context.Orders
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();

            return View(orders);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.User) // nếu muốn hiện thông tin user
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)  // load sản phẩm nếu có
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Combo)    // load combo nếu có
                .Include(o => o.DeliveryRequest)    // nếu cần thông tin giao hàng
                .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();

            var vm = new OrderStatusEditViewModel
            {
                OrderID = order.OrderID,
                CurrentStatus = order.Status,
                NewStatus = order.Status
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderStatusEditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Combo)
                    .ThenInclude(c => c.ComboItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(o => o.OrderID == model.OrderID);

            if (order == null)
                return NotFound();

            var oldStatus = order.Status;
            var newStatus = model.NewStatus;

            var statusListToSubtract = new List<string> { "Pending", "Shipping", "Delivered" };

            // Nếu trạng thái mới là Pending, Shipping, Delivered và trạng thái cũ chưa trừ tồn thì trừ tồn kho
            if (statusListToSubtract.Contains(newStatus) && !statusListToSubtract.Contains(oldStatus))
            {
                foreach (var detail in order.OrderDetails)
                {
                    if (detail.ProductID.HasValue)
                    {
                        var product = detail.Product;
                        if (product != null)
                        {
                            product.Quantity -= detail.Quantity;
                            if (product.Quantity < 0) product.Quantity = 0;
                            _context.Products.Update(product);
                        }
                    }
                    else if (detail.ComboID.HasValue)
                    {
                        var combo = detail.Combo;
                        if (combo != null && combo.ComboItems != null)
                        {
                            foreach (var comboItem in combo.ComboItems)
                            {
                                var productInCombo = comboItem.Product;
                                if (productInCombo != null)
                                {
                                    productInCombo.Quantity -= detail.Quantity * comboItem.Quantity;
                                    if (productInCombo.Quantity < 0) productInCombo.Quantity = 0;
                                    _context.Products.Update(productInCombo);
                                }
                            }
                        }
                    }
                }
            }
            // Nếu trạng thái mới là Canceled và trạng thái cũ thuộc nhóm đã trừ tồn thì hoàn trả tồn kho
            else if (newStatus == "Canceled" && statusListToSubtract.Contains(oldStatus))
            {
                foreach (var detail in order.OrderDetails)
                {
                    if (detail.ProductID.HasValue)
                    {
                        var product = detail.Product;
                        if (product != null)
                        {
                            product.Quantity += detail.Quantity;
                            _context.Products.Update(product);
                        }
                    }
                    else if (detail.ComboID.HasValue)
                    {
                        var combo = detail.Combo;
                        if (combo != null && combo.ComboItems != null)
                        {
                            foreach (var comboItem in combo.ComboItems)
                            {
                                var productInCombo = comboItem.Product;
                                if (productInCombo != null)
                                {
                                    productInCombo.Quantity += detail.Quantity * comboItem.Quantity;
                                    _context.Products.Update(productInCombo);
                                }
                            }
                        }
                    }
                }
            }

            order.Status = newStatus;

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cập nhật trạng thái đơn hàng thành công!";
            return RedirectToAction(nameof(Index));
        }

    }
}
