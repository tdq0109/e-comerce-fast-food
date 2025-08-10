using ASM.Models.ViewModels.User;
using AutoMapper;

namespace ASM.Models.AutoMapperConfiguration
{
    public class UserMap : Profile
    {
        public UserMap()
        {
            CreateMap<UserRegisterViewModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore()); // xử lý hash ngoài AutoMapper
            CreateMap<User, UserProfileViewModel>().ReverseMap();

            CreateMap<UserLoginViewModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<UserCreateViewModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
        }
    }
}
