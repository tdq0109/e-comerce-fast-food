using ASM.Models;
using ASM.Models.ViewModels.Cart;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private int? GetUserId() => HttpContext.Session.GetInt32("UserID");

        public CartController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = await _context.Carts
                .Where(c => c.UserID == userId)
                .Include(c => c.Product)
                .Include(c => c.Combo)
                .ToListAsync();
            var cartItemViewModels = _mapper.Map<List<CartItemViewModel>>(cartItems);
            var viewModel = new CartSummaryViewModel
            {
                Items = cartItemViewModels,
                TotalPrice = cartItemViewModels.Sum(i => i.Price * i.Quantity)
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
                return RedirectToAction("Login", "Account");

            var cartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);

            if (cartItem != null)
            {
                cartItem.Quantity += 1;
            }
            else
            {
                _context.Carts.Add(new Cart
                {
                    UserID = userId.Value,
                    ProductID = productId,
                    ComboID = null, // Giỏ hàng chỉ chứa sản phẩm hoặc combo, không phải cả hai
                    Quantity = 1
                });
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm vào giỏ hàng thành công!";
            return RedirectToAction("Index", "Home"); // hoặc trở lại trang hiện tại
        }
        [HttpPost]
        public IActionResult Increase(int? productId, int? comboId)
        {
            var userId = GetUserId();
            var item = _context.Carts.FirstOrDefault(c => c.UserID == userId && c.ProductID == productId && c.ComboID == comboId);
            if (item != null)
            {
                item.Quantity++;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Decrease(int? productId, int? comboId)
        {
            var userId = GetUserId();
            var item = _context.Carts.FirstOrDefault(c => c.UserID == userId && c.ProductID == productId && c.ComboID == comboId);
            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                    _context.Carts.Remove(item);

                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Remove(int? productId, int? comboId)
        {
            var userId = GetUserId();
            var item = _context.Carts.FirstOrDefault(c => c.UserID == userId && c.ProductID == productId && c.ComboID == comboId);
            if (item != null)
            {
                _context.Carts.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AddComboToCart(int comboId, int quantity = 1)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var existingCartItem = await _context.Carts
                .FirstOrDefaultAsync(c => c.UserID == userId && c.ComboID == comboId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += 1;
            }
            else
            {
                _context.Carts.Add (new Cart
                {
                    UserID = userId.Value,
                    ComboID = comboId,
                    ProductID = null,
                    Quantity = 1
                });
            }
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm vào giỏ hàng thành công!";

            return RedirectToAction("Index", "Home"); // hoặc chỗ hiển thị giỏ hàng
        }


    }
}
