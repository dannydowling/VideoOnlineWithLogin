using AutoMapper;
using PreFlight_API.BLL.Models;
using PreFlight_API.DAL.MySql.Models;
using System;

namespace PreFlight_API.BLL.Mappings
{
    public class WeatherMapping : Profile
    {
        public WeatherMapping()
        {
            CreateMap<Weather, WeatherEntity>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(d => d.AirPressure, opt => opt.MapFrom(src => src.AirPressure))
                .ForMember(d => d.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(d => d.WeightValue, opt => opt.MapFrom(src => src.WeightValue))
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(src => src.RowVersion));

            CreateMap<WeatherEntity, Weather>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(d => d.AirPressure, opt => opt.MapFrom(src => src.AirPressure))
                .ForMember(d => d.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(d => d.WeightValue, opt => opt.MapFrom(src => src.WeightValue))
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(src => src.RowVersion));
        }
    }
}
