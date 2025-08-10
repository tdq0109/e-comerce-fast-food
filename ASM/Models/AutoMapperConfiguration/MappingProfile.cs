using ASM.Models.ViewModels.Cart;
using ASM.Models.ViewModels.Combo;
using ASM.Models.ViewModels.Feedback;
using ASM.Models.ViewModels.Order;
using ASM.Models.ViewModels.Product;
using ASM.Models.ViewModels.User;
using AutoMapper;

namespace ASM.Models.AutoMapperConfiguration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // -------- USER --------
            CreateMap<UserRegisterViewModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // xử lý hash ngoài AutoMapper
            CreateMap<User, UserProfileViewModel>().ReverseMap();

            CreateMap<UserLoginViewModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // -------- PRODUCT --------
            CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
            CreateMap<Product, ProductDetailViewModel>().ReverseMap();

            // -------- COMBO --------
            CreateMap<Combo, ComboViewModel>().ReverseMap();
            CreateMap<Combo, ComboDetailViewModel>()
                .ForMember(dest => dest.Items, opt => opt.Ignore()); // xử lý thủ công
            CreateMap<ComboDetailViewModel, Combo>()
                .ForMember(dest => dest.ComboItems, opt => opt.Ignore());

            // -------- ORDER --------
            CreateMap<OrderCreateViewModel, Order>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.Items));
            CreateMap<Order, OrderCreateViewModel>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());

            CreateMap<OrderItemCreateViewModel, OrderDetail>().ReverseMap();

            CreateMap<Order, OrderHistoryViewModel>();
            CreateMap<OrderHistoryViewModel, Order>()
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());

            // -------- CART --------
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

            // -------- FEEDBACK --------
            CreateMap<FeedbackCreateViewModel, Feedback>();
            CreateMap<Feedback, FeedbackViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName));
            CreateMap<FeedbackViewModel, Feedback>()
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
