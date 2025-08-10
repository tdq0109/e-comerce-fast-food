using ASM.Models.ViewModels.Cart;
using ASM.Models.ViewModels.Order;
using AutoMapper;

namespace ASM.Models.AutoMapperConfiguration
{
    public class CartMap : Profile
    {
        public CartMap()
        {
            CreateMap<Cart, CartItemViewModel>()
                .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.ComboID, opt => opt.MapFrom(src => src.ComboID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    src.Product != null ? src.Product.ProductName :
                    (src.Combo != null ? src.Combo.ComboName : "")
                ))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src =>
                    src.Product != null ? src.Product.Price :
                    (src.Combo != null ? src.Combo.Price : 0)
                ))
                .ForMember(dest => dest.ImageURL, opt => opt.MapFrom(src =>
                    src.Product != null ? src.Product.ImageURL :
                    (src.Combo != null ? src.Combo.ImageUrl : "")
                ));

            CreateMap<CartItemViewModel, Cart>()
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Combo, opt => opt.Ignore());
            CreateMap<CartItemViewModel, OrderItemCreateViewModel>()
    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name));
        }
    }
}
