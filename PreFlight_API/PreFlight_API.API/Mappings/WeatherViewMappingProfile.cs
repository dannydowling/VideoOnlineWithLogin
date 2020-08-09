using AutoMapper;
using System;

namespace PreFlight_API.API.Mappings
{
    public class WeatherViewMapping : Profile
    {
        public WeatherViewMapping()
        {
            CreateMap<BLL.Models.Weather, Models.Weather>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.AirPressure, opt => opt.MapFrom(src => src.AirPressure))
                .ForMember(d => d.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(d => d.WeightValue, opt => opt.MapFrom(src => src.WeightValue))
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(src => src.RowVersion));

            CreateMap<Models.Weather, BLL.Models.Weather>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id.ToString())))
                .ForMember(d => d.AirPressure, opt => opt.MapFrom(src => src.AirPressure))
                .ForMember(d => d.Temperature, opt => opt.MapFrom(src => src.Temperature))
                .ForMember(d => d.WeightValue, opt => opt.MapFrom(src => src.WeightValue))
                .ForMember(d => d.RowVersion, opt => opt.MapFrom(src => src.RowVersion));
        }
    }
}
