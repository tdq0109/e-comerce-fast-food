using ASM.Models;
using ASM.Models.ViewModels.Feedback;
using ASM.Models.ViewModels.Product;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("/Product/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var product = _context.Products
             .Include(p => p.Category)
             .Where(p => p.ProductID == id)
             .Select(p => new ProductDetailViewModel
             {
                 ProductID = p.ProductID,
                 ProductName = p.ProductName,
                 Description = p.Description,
                 Price = p.Price,
                 Quantity = p.Quantity,
                 ImageURL = p.ImageURL,
                 CategoryName = p.Category.CategoryName,
                 Tags = p.Tags,
                 IsCombo = p.IsCombo,
                 IsAvailable = p.IsAvailable,
                 Feedbacks = p.Feedbacks.Select(f => new FeedbackViewModel
                 {
                     Comment = f.Comment,
                     Rating = f.Rating
                 }).ToList()
             }).FirstOrDefault();

            if (product == null) return NotFound();

            var productDetailVM = _mapper.Map<ProductDetailViewModel>(product);
            return View(productDetailVM);
        }
        //[HttpGet]
        //public async Task<IActionResult> Search(string keyword)
        //{
        //    if (string.IsNullOrWhiteSpace(keyword))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    var result = await _context.Products
        //        .Where(p => p.ProductName.Contains(keyword) && p.IsAvailable)
        //        .ToListAsync();

        //    var resultVM = _mapper.Map<List<ProductViewModel>>(result);

        //    return View("SearchResults", resultVM);
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
                        products = Enumerable.Empty<Product>().AsQueryable(); // Không khớp
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
            return View("SearchResults", result); // hoặc trả về View phù hợp
        }

    }
}
