using AutoMapper;
using PreFlight_API.BLL.Models;
using PreFlight_API.DAL.MySql.Models;
using System;

namespace PreFlight_API.BLL.Mappings
{
    public class EmployeeMapping : Profile
    {
        public EmployeeMapping()
        {
            CreateMap<Employee, EmployeeEntity>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(d => d.JobCategoryId, opt => opt.MapFrom(src => src.JobCategoryId.ToString()))
                .ForMember(d => d.JoinedDate, opt => opt.MapFrom(src => src.HireDate))
                .ForMember(d => d.employeeLocations, opt => opt.MapFrom(src => src.employeeLocations))
                .ForMember(d => d.employeeWeatherList, opt => opt.MapFrom(src => src.overrideWeatherList));

            CreateMap<EmployeeEntity, Employee>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id.ToString())))
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(d => d.JobCategoryId, opt => opt.MapFrom(src => (JobCategory)src.JobCategoryId))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(d => d.HireDate, opt => opt.MapFrom(src => src.JoinedDate))
                .ForMember(d => d.employeeLocations, opt => opt.MapFrom(src => src.employeeLocations))
                .ForMember(d => d.overrideWeatherList, opt => opt.MapFrom(src => src.employeeWeatherList));
                
        }
    }
}
