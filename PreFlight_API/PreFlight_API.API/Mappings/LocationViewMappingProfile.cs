using AutoMapper;

namespace PreFlight_API.API.Mappings
{
    public class LocationViewMapping : Profile
    {
        public LocationViewMapping()
        {
            CreateMap<BLL.Models.Location, Models.Location>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(d => d.Zip, opt => opt.MapFrom(src => src.Zip))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.City));

            CreateMap<Models.Location, BLL.Models.Location>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(d => d.Zip, opt => opt.MapFrom(src => src.Zip))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.City));
        }
    }
}
