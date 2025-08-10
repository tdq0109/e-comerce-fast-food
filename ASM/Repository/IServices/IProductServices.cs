using ASM.Models.ViewModels.Product;

namespace ASM.Repository.IServices
{
    public interface IProductServices
    {
        public Task<List<ProductViewModel>> GetAllAsync();
    }
}
