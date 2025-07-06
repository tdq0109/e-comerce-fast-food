using ASM.Models.ViewModels.Product;
using AutoMapper;

namespace ASM.Models.AutoMapperConfiguration
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            CreateMap<Product, ProductDetailViewModel>().ReverseMap();
        }
    }
}
