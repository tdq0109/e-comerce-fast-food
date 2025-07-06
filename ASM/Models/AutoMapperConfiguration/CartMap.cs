using ASM.Models.ViewModels.Cart;
using AutoMapper;

namespace ASM.Models.AutoMapperConfiguration
{
    public class CartMap : Profile
    {
        public CartMap()
        {
            CreateMap<Cart, CartItemViewModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src => src.Product.ImageURL));
            CreateMap<CartItemViewModel, Cart>()
                .ForMember(dest => dest.Product, opt => opt.Ignore());
        }
    }
}
