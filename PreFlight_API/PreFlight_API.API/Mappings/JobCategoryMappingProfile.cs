using AutoMapper;
using PreFlight_API.BLL.Models;
using PreFlight_API.DAL.MySql.Models;
using System;

namespace PreFlight_API.BLL.Mappings
{
    public class JobCategoryViewMappings : Profile
    {
        public JobCategoryViewMappings()
        {
            CreateMap<BLL.Models.JobCategory, Models.JobCategory>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(d => d.JobCategoryName, opt => opt.MapFrom(src => src.JobCategoryName));

            CreateMap<Models.JobCategory, BLL.Models.JobCategory>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id.ToString())))
                .ForMember(d => d.JobCategoryName, opt => opt.MapFrom(src => src.JobCategoryName));
                
        }
    }
}
