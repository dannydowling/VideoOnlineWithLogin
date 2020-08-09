using AutoMapper;
using PreFlight_API.BLL.Models;
using PreFlight_API.DAL.MySql.Models;
using System;

namespace PreFlight_API.BLL.Mappings
{
    public class LocationMapping : Profile
    {
        public LocationMapping()
        {
            CreateMap<Location, LocationEntity>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(d => d.Zip, opt => opt.MapFrom(src => src.Zip))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.City));

            CreateMap<LocationEntity, Location>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Street))
                .ForMember(d => d.Zip, opt => opt.MapFrom(src => src.Zip))
                .ForMember(d => d.Country, opt => opt.MapFrom(src => src.Country))
                .ForMember(d => d.City, opt => opt.MapFrom(src => src.City));
        }
    }
}
