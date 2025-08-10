using ASM.Models.ViewModels.Combo;
using ASM.Models.ViewModels.Product;
using ASM.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASM.Controllers
{
    public class ComboController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ComboController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("/Combo/Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var combo = await _context.Combos
                .Include(c => c.ComboItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.ComboID == id);

            if (combo == null) return NotFound();

            var comboVM = _mapper.Map<ComboDetailViewModel>(combo);
            comboVM.Items = combo.ComboItems
                .Select(ci => _mapper.Map<ProductViewModel>(ci.Product))
                .ToList();

            return View(comboVM);
        }
    }
}
