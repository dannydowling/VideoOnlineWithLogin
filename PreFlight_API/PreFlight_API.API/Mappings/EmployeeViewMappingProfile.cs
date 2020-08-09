using AutoMapper;

namespace PreFlight_API.API.Mappings
{
    public class EmployeeViewMappings : Profile
    {
        public EmployeeViewMappings()
        {
            CreateMap<BLL.Models.Employee, Models.Employee>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(d => d.JobCategoryId, opt => opt.MapFrom(src => src.JobCategoryId))
                .ForMember(d => d.JoinedDate, opt => opt.MapFrom(src => src.HireDate))
                .ForMember(d => d.employeeLocations, opt => opt.MapFrom(src => src.employeeLocations))
                .ForMember(d => d.employeeWeatherList, opt => opt.MapFrom(src => src.overrideWeatherList));

            CreateMap<Models.Employee, BLL.Models.Employee>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(d => d.JobCategoryId, opt => opt.MapFrom(src => src.JobCategoryId))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(d => d.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(d => d.HireDate, opt => opt.MapFrom(src => src.JoinedDate))
                .ForMember(d => d.employeeLocations, opt => opt.MapFrom(src => src.employeeLocations))
                .ForMember(d => d.overrideWeatherList, opt => opt.MapFrom(src => src.employeeWeatherList));
                
        }
    }
}
