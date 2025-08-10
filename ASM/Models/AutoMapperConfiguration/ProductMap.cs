using ASM.Models.ViewModels.Product;
using AutoMapper;

namespace ASM.Models.AutoMapperConfiguration
{
    public class ProductMap : Profile
    {
        public ProductMap()
        {

            // Map Product to ProductViewModel
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));

            // Map Product to ProductDetailViewModel
            CreateMap<Product, ProductDetailViewModel>()
                        .IncludeBase<Product, ProductViewModel>() // Inherit base mapping
                        .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                        .ForMember(dest => dest.Topic, opt => opt.MapFrom(src => src.Topic))
                        .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
                        .ForMember(dest => dest.Feedbacks, opt => opt.Ignore());

        }
    }

}
