using ASM.Models;
using ASM.Models.ViewModels.Product;
using ASM.Repository.IServices;
using AutoMapper;

namespace ASM.Repository.Services
{
    public class ProductServices : IProductServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public Task<List<ProductViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
