using ASM.Models.ViewModels.Feedback;
using AutoMapper;

namespace ASM.Models.AutoMapperConfiguration
{
    public class FeedbackMap : Profile
    {
        public FeedbackMap() 
        {
            CreateMap<FeedbackCreateViewModel, Feedback>();
            CreateMap<Feedback, FeedbackViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.User.FullName));
            CreateMap<FeedbackViewModel, Feedback>()
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
