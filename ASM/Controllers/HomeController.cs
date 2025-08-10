using ASM.Models;
using ASM.Models.ViewModels.Combo;
using ASM.Models.ViewModels.Product;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsAvailable && !p.IsCombo) 
                .ToListAsync();

            var productViewModels = _mapper.Map<List<ProductViewModel>>(products);

           
            var combos = await _context.Combos.Where(c => c.IsAvailable).ToListAsync();
            var combosVM = _mapper.Map<List<ComboViewModel>>(combos);
            ViewBag.Combos = combosVM;
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = categories;

            return View(productViewModels);
        }
        //[HttpGet]
        //public async Task<IActionResult> Search(string keyword)
        //{
        //    if (string.IsNullOrWhiteSpace(keyword))
        //    {
        //        return RedirectToAction("Menu");
        //    }

        //    var matchedProducts = await _context.Products
        //        .Include(p => p.Category)
        //        .Where(p => p.IsAvailable && !p.IsCombo && p.ProductName.Contains(keyword))
        //        .ToListAsync();

        //    var productVMs = _mapper.Map<List<ProductViewModel>>(matchedProducts);

        //    var combos = await _context.Combos.ToListAsync();
        //    var comboVMs = _mapper.Map<List<ComboViewModel>>(combos);
        //    ViewBag.Combos = comboVMs;

        //    ViewBag.Keyword = keyword;
        //    return View("Menu", productVMs); 
        //}
        [HttpGet]
        public IActionResult FieldSearch(string field, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return RedirectToAction("Menu");

            var products = _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsAvailable);

            switch (field)
            {
                case "ProductName":
                    products = products.Where(p => p.ProductName.Contains(keyword));
                    break;
                case "Price":
                    if (decimal.TryParse(keyword, out var price))
                    {
                        products = products.Where(p => p.Price == price);
                    }
                    else
                    {
                        products = Enumerable.Empty<Product>().AsQueryable(); // Không kh?p
                    }
                    break;
                case "CategoryName":
                    products = products.Where(p => p.Category.CategoryName.Contains(keyword));
                    break;
                case "Tags":
                    products = products.Where(p => p.Tags.Contains(keyword));
                    break;
                default:
                    break;
            }

            var result = _mapper.Map<List<ProductViewModel>>(products.ToList());
            return View("Menu", result); // ho?c tr? v? View phù h?p
        }


    }
}
