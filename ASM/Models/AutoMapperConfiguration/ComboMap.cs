using ASM.Models.ViewModels.Combo;
using AutoMapper;

namespace ASM.Models.AutoMapperConfiguration
{
    public class ComboMap : Profile
    {
        public ComboMap()
        {
            CreateMap<Combo, ComboViewModel>().ReverseMap();
            CreateMap<Combo, ComboDetailViewModel>()
                .ForMember(dest => dest.Items, opt => opt.Ignore()); // điền thủ công nếu cần
            CreateMap<ComboDetailViewModel, Combo>()
    .ForMember(dest => dest.ComboItems, opt => opt.Ignore());
        }
    }
}
