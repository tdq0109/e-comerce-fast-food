using ASM.Models.ViewModels.Category;
using AutoMapper;

namespace ASM.Models.AutoMapperConfiguration
{
    public class CategoryMap : Profile
    {
        public CategoryMap()
        {
            CreateMap<Category, CategoryViewModel>()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CategoryName));
        }
    }
}
