using ASM.Models.ViewModels.User;
using AutoMapper;

namespace ASM.Models.AutoMapperConfiguration
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<UserRegisterViewModel, User>();
            CreateMap<User, UserProfileViewModel>().ReverseMap();
        }
    }
}
