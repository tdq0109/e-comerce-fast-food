using ASM.Models;
using ASM.Models.ViewModels.Order;
using ASM.Models.ViewModels.Product;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;

namespace ASM.Controllers
{
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private int? GetUserId()
        {
            return HttpContext.Session.GetInt32("UserID");
        }


        public OrderController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            var userId = GetUserId();
            if (userId == null) return RedirectToAction("Login", "Account");

            var cartItems = _context.Carts
                .Include(c => c.Product)
                .Include(c => c.Combo)
                .Where(c => c.UserID == userId)
                .ToList();

            if (!cartItems.Any())
            {
                TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index");
            }
            var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
            var model = new OrderCreateViewModel
            {
                DeliveryAddress = user?.Address ?? string.Empty,
                Items = cartItems.Select(c => new OrderItemCreateViewModel
                {
                    ProductID = c.ProductID,
                    ComboID = c.ComboID,
                    Quantity = c.Quantity,
                    ProductName = c.Product?.ProductName ?? c.Combo?.ComboName ?? "Không xác định",
                    Price = c.Product?.Price ?? c.Combo?.Price ?? 0
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Checkout(OrderCreateViewModel model, string Note)
        {
            var userId = GetUserId();
            if (userId == null)
                return RedirectToAction("Login", "Account");

            if (model.Items == null || !model.Items.Any())
            {
                TempData["Error"] = "Giỏ hàng của bạn đang trống!";
                return RedirectToAction("Index", "Cart");
            }

            decimal totalAmount = 0;
            var orderDetails = new List<OrderDetail>();

            foreach (var item in model.Items)
            {
                decimal price = 0;

                if (item.ProductID.HasValue) // Là sản phẩm lẻ
                {
                    var product = _context.Products
                                           .FirstOrDefault(p => p.ProductID == item.ProductID.Value);
                    if (product == null) continue;

                    price = product.Price;
                }
                else if (item.ComboID.HasValue) // Là combo
                {
                    var combo = _context.Combos
                                        .FirstOrDefault(c => c.ComboID == item.ComboID.Value);
                    if (combo == null) continue;

                    price = combo.Price; // Giá của combo, không tính từ Product
                }

                totalAmount += price * item.Quantity;

                orderDetails.Add(new OrderDetail
                {
                    ProductID = item.ProductID,
                    ComboID = item.ComboID,
                    Quantity = item.Quantity,
                    UnitPrice = price
                });
                foreach (var detail in orderDetails)
                {
                    if (detail.ProductID.HasValue)
                    {
                        var product = _context.Products.FirstOrDefault(p => p.ProductID == detail.ProductID.Value);
                        if (product != null)
                        {
                            product.Quantity -= detail.Quantity;
                            if (product.Quantity < 0) product.Quantity = 0;
                        }
                    }
                    else if (detail.ComboID.HasValue)
                    {
                        var comboItems = _context.ComboItems
                                                 .Where(ci => ci.ComboID == detail.ComboID.Value)
                                                 .ToList();

                        foreach (var comboItem in comboItems)
                        {
                            var product = _context.Products.FirstOrDefault(p => p.ProductID == comboItem.ProductID);
                            if (product != null)
                            {
                                int quantityToDeduct = comboItem.Quantity * detail.Quantity;
                                product.Quantity -= quantityToDeduct;
                                if (product.Quantity < 0) product.Quantity = 0;
                            }
                        }
                    }
                }


            }

            if (!orderDetails.Any())
            {
                TempData["Error"] = "Không có sản phẩm hợp lệ trong đơn hàng.";
                return RedirectToAction("Index", "Cart");
            }

            var order = new Order
            {
                UserID = userId.Value,
                DeliveryAddress = model.DeliveryAddress,
                OrderDate = DateTime.Now,
                Status = "Pending",
                Note = Note,
                TotalAmount = totalAmount,
                OrderDetails = orderDetails
            };

            try
            {
                _context.Orders.Add(order);

                // Xóa giỏ hàng sau khi đặt
                var cartItems = _context.Carts.Where(c => c.UserID == userId.Value).ToList();
                if (cartItems.Any())
                {
                    _context.Carts.RemoveRange(cartItems);
                }

                _context.SaveChanges();

                TempData["Success"] = "Đặt hàng thành công!";
                return RedirectToAction("OrderHistory", "Order");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction("Index", "Cart");
            }
        }
        // Hiển thị trang OrderHistory
        [HttpGet]
        public IActionResult OrderHistory()
        {
            var userId = GetUserId();

            var orders = _context.Orders
                .Where(o => o.UserID == userId)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderHistoryViewModel
                {
                    OrderID = o.OrderID,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    DeliveryAddress = o.DeliveryAddress
                })
                .ToList();

            ViewBag.AllOrders = orders;

            return View();
        }
        [HttpGet]
        public IActionResult GetOrderDetail(int id)
        {
            // Lấy danh sách đơn hàng (cho bảng lịch sử)
            var allOrders = _context.Orders
                .Select(o => new OrderHistoryViewModel
                {
                    OrderID = o.OrderID,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    TotalAmount = o.TotalAmount,
                    DeliveryAddress = o.DeliveryAddress
                }).ToList();

            ViewBag.AllOrders = allOrders;

            // Lấy chi tiết đơn hàng
            var order = _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Combo)
                .FirstOrDefault(o => o.OrderID == id);

            if (order == null) return NotFound();

            var vm = new OrderCreateViewModel
            {
                DeliveryAddress = order.DeliveryAddress,
                Note = order.Note,
                Items = order.OrderDetails.Select(od => new OrderItemCreateViewModel
                {
                    ProductID = od.ProductID,
                    ComboID = od.ComboID,
                    Quantity = od.Quantity,
                    Price = od.UnitPrice,
                    ProductName = od.Product != null ? od.Product.ProductName : od.Combo.ComboName
                }).ToList()
            };

            return View("OrderHistory", vm);
        }
        [HttpGet]
        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            var model = new CancelOrderViewModel
            {
                OrderID = id
            };

            return View(model); // Trả về view CancelOrder.cshtml
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelOrder(CancelOrderViewModel model)
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

            order.Status = "Canceled";
            order.CancelReason = model.CancelReason;

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

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đơn hàng {order.OrderID} đã được hủy và trả lại tồn kho.";
            return RedirectToAction("OrderHistory", "Order");
        }

    }

}
