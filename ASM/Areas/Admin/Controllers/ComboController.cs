using ASM.Models.ViewModels.Product;
using ASM.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASM.Models.ViewModels.Combo;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace ASM.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ComboController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ComboController(AppDbContext context, IWebHostEnvironment environment)
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
            var combos = await _context.Combos
                .AsNoTracking()
                .ToListAsync();

            // Map sang ViewModel
            var comboVMs = combos.Select(p => new ComboViewModel
            {
                ComboID = p.ComboID,
                ComboName = p.ComboName,
                Description = p.Description,
                Price = p.Price,
                ImageUrl = p.ImageUrl,
                IsAvailable = p.IsAvailable
            }).ToList();

            return View(comboVMs);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new ComboCreateViewModel();
            vm.ItemsCombo.Items.Add(new ProductViewModel()); // ít nhất 1 dòng mặc định

            ViewBag.Products = new SelectList(_context.Products, "ProductID", "ProductName");
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComboCreateViewModel model, string action, int? removeIndex)
        {

            if (action == "add")
            {
                model.ItemsCombo.Items.Add(new ProductViewModel());
            }
            else if (action == "remove" && removeIndex.HasValue && removeIndex.Value >= 0 && removeIndex.Value < model.ItemsCombo.Items.Count)
            {
                model.ItemsCombo.Items.RemoveAt(removeIndex.Value);
            }
            else if (action == "save")
            {
                if (ModelState.IsValid)
                {
                    // Tạo entity Combo
                    var combo = new Combo
                    {
                        ComboName = model.ComboName,
                        Description = model.Description,
                        Price = model.Price,
                        IsAvailable = model.IsAvailable
                    };

                    // Lưu ảnh nếu có
                    if (model.Image != null && model.Image.Length > 0)
                    {
                        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "combos");
                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await model.Image.CopyToAsync(fileStream);
                        }

                        combo.ImageUrl = $"/uploads/combos/{uniqueFileName}";
                    }

                    _context.Combos.Add(combo);
                    await _context.SaveChangesAsync(); // Lưu để có ComboID

                    // Lưu danh sách sản phẩm trong combo
                    if (model.ItemsCombo?.Items != null)
                    {
                        foreach (var item in model.ItemsCombo.Items)
                        {
                            if (item.ProductID > 0 && item.Quantity > 0)
                            {
                                _context.ComboItems.Add(new ComboItem
                                {
                                    ComboID = combo.ComboID,
                                    ProductID = item.ProductID,
                                    Quantity = item.Quantity
                                });
                            }
                        }
                        await _context.SaveChangesAsync();
                    }

                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.Products = new SelectList(_context.Products, "ProductID", "ProductName");
            return View(model);
           
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var combo = _context.Combos
                .Include(c => c.ComboItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.ComboID == id);

            if (combo == null)
            {
                return NotFound();
            }

            var vm = new ComboEditViewModel
            {
                ComboName = combo.ComboName,
                Description = combo.Description,
                Price = combo.Price,
                IsAvailable = combo.IsAvailable,
                ImageUrl = combo.ImageUrl,
                ItemsCombo = new ComboItemCreateViewModel
                {
                    Items = combo.ComboItems.Select(ci => new ProductViewModel
                    {
                        ProductID = ci.ProductID,
                        ProductName = ci.Product.ProductName,
                        Quantity = ci.Quantity
                    }).ToList()
                }
            };

            // Nếu cần dropdown để thêm sản phẩm mới
            ViewBag.Products = new SelectList(_context.Products.ToList(), "ProductID", "ProductName");


            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ComboEditViewModel model, string action, int? removeIndex)
        {
            ViewBag.Products = new SelectList(_context.Products.ToList(), "ProductID", "ProductName");


            if (!ModelState.IsValid)
                return View(model);

            if (action == "add")
            {
                model.ItemsCombo.Items.Add(new ProductViewModel());
                return View(model);
            }
            else if (action == "remove" && removeIndex.HasValue
                     && removeIndex.Value >= 0 && removeIndex.Value < model.ItemsCombo.Items.Count)
            {
                model.ItemsCombo.Items.RemoveAt(removeIndex.Value);
                return View(model);
            }
            else if (action == "save")
            {
                var combo = await _context.Combos
                    .Include(c => c.ComboItems)
                    .FirstOrDefaultAsync(c => c.ComboID == id);

                if (combo == null)
                    return NotFound();

                // Cập nhật thông tin combo
                combo.ComboName = model.ComboName;
                combo.Description = model.Description;
                combo.Price = model.Price;
                combo.IsAvailable = model.IsAvailable;

                // Lưu ảnh nếu có
                if (model.Image != null && model.Image.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads", "combos");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(fileStream);
                    }

                    combo.ImageUrl = $"/uploads/combos/{uniqueFileName}";
                }

                // Xóa toàn bộ items cũ
                _context.ComboItems.RemoveRange(combo.ComboItems);

                // Thêm items mới
                if (model.ItemsCombo?.Items != null)
                {
                    foreach (var item in model.ItemsCombo.Items)
                    {
                        if (item.ProductID > 0 && item.Quantity > 0)
                        {
                            _context.ComboItems.Add(new ComboItem
                            {
                                ComboID = combo.ComboID,
                                ProductID = item.ProductID,
                                Quantity = item.Quantity
                            });
                        }
                    }
                }

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Cập nhật thông tin Combo thành công!";
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null) return RedirectToAction("Login", "Account");
            var combo = await _context.Combos.FindAsync(id);
            if (combo == null)
                return NotFound();

            _context.Combos.Remove(combo);
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

            var combo = await _context.Combos.FindAsync(id);
            if (combo == null) return NotFound();

            combo.IsAvailable = !combo.IsAvailable;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Đã {(combo.IsAvailable ? "kích hoạt" : "hủy kích hoạt")} Combo : {combo.ComboName}";

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var combo = _context.Combos
                .Include(c => c.ComboItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefault(c => c.ComboID == id);

            if (combo == null)
            {
                return NotFound();
            }

            return View(combo);
        }


    }
}
