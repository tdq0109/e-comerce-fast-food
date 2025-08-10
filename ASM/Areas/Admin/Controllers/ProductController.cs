using ASM.Models;
using ASM.Models.ViewModels.Product;
using ASM.Models.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.PowerBI.Api;
using Microsoft.PowerBI.Api.Models;

namespace ASM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProductController(AppDbContext context, IWebHostEnvironment environment)
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

            // Lấy toàn bộ sản phẩm (không cần lấy FindAsync theo userId, vì đây là danh sách)
            var products = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            // Map sang ViewModel
            var productVMs = products.Select(p => new ProductViewModel
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                Price = p.Price,
                Quantity = p.Quantity,
                ImageURL = p.ImageURL,
                IsAvailable = p.IsAvailable
            }).ToList();

            return View(productVMs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");
            if (ModelState.IsValid)
            {
                // Lấy Category từ DB
                var category = await _context.Categories
                                             .FirstOrDefaultAsync(c => c.CategoryID == model.CategoryID);

                if (category == null)
                {
                    ModelState.AddModelError("CategoryID", "Danh mục không tồn tại.");
                    ViewBag.Categories = _context.Categories.ToList();
                    return View(model);
                }

                var product = new Product
                {
                    ProductName = model.ProductName,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    CategoryID = model.CategoryID,
                    IsAvailable = model.IsAvailable,
                    CreatedAt = DateTime.Now,

                    // Tự động tạo Tag từ CategoryName
                    Tags = GenerateTag(category.CategoryName)
                };
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "products");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ImageFile.CopyToAsync(fileStream);
                    }

                    product.ImageURL = $"/uploads/products/{uniqueFileName}";
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = _context.Categories.ToList();
            return View(model);
        }

        // Hàm loại bỏ dấu và ký tự không mong muốn
        private string GenerateTag(string input)
        {
            var normalized = input.Normalize(System.Text.NormalizationForm.FormD);
            var chars = normalized.Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark);
            var clean = new string(chars.ToArray());

            return clean.ToLower()
                        .Replace(" ", "")
                        .Replace("-", "");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");
            var product = _context.Products
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            var vm = new ProductEditViewModel
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                ImageURL = product.ImageURL,
                CategoryID = product.CategoryID,
                IsAvailable = product.IsAvailable,
            };

            ViewBag.Categories = new SelectList(
                _context.Categories,
                "CategoryID",
                "CategoryName",
                product.CategoryID
            );

            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditViewModel model)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");
            //if (id <= 0)
            //    return BadRequest();

            if (!ModelState.IsValid)
                return View(model);

            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.ProductName = model.ProductName;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.CategoryID = model.CategoryID;
            product.IsAvailable = model.IsAvailable;
            var category = _context.Categories.FirstOrDefault(c => c.CategoryID == model.CategoryID);
            if (category != null)
            {
                product.Tags = category.CategoryName
                    .ToLower()
                    .Replace(" ", "")
                    .Replace("-", "");
            }

            ViewBag.Categories = new SelectList(
                _context.Categories,
                "CategoryID",
                "CategoryName",
                product.CategoryID
            );

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "products");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                product.ImageURL = $"/uploads/products/{uniqueFileName}";
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cập nhật thông tin sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");
            var product = _context.Products
                .Include(p => p.Category) // ✅ load cả Category
                .FirstOrDefault(p => p.ProductID == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Xóa sản phẩm thành công!";
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");
            //if (id <= 0) return NotFound();

            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            product.IsAvailable = !product.IsAvailable;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã {(product.IsAvailable ? "kích hoạt" : "hủy kích hoạt")} sản phẩm : {product.ProductName}";

            return RedirectToAction(nameof(Index));
        }
    }
}
