using ASM.Models.ViewModels.Order;
using AutoMapper;

namespace ASM.Models.AutoMapperConfiguration
{
    public class OrderMap : Profile
    {
        public OrderMap()
        {
            CreateMap<OrderCreateViewModel, Order>()
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.Items));
            CreateMap<Order, OrderCreateViewModel>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());

            CreateMap<OrderItemCreateViewModel, OrderDetail>().ReverseMap();

            CreateMap<Order, OrderHistoryViewModel>()
                .ForMember(dest => dest.Items, opt => opt.Ignore());
            CreateMap<OrderHistoryViewModel, Order>()
                .ForMember(dest => dest.OrderDetails, opt => opt.Ignore());
        }
    }
}
