using AutoMapper;

namespace PreFlight_API.API.Mappings
{
    public class UserViewMapping : Profile
    {
        public UserViewMapping()
        {
            CreateMap<BLL.Models.UserModel, Models.UserModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(d => d.Weathers, opt => opt.MapFrom(src => src.Weathers))
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(d => d.Comment, opt => opt.MapFrom(src => src.Comment));

            CreateMap<Models.UserModel, BLL.Models.UserModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))                
                .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(d => d.Weathers, opt => opt.MapFrom(src => src.Weathers))
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(d => d.Comment, opt => opt.MapFrom(src => src.Comment));
        }
    }
}
