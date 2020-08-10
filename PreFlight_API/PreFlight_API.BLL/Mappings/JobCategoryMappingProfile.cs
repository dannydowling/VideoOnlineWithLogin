using AutoMapper;
using PreFlight_API.BLL.Models;
using PreFlight_API.DAL.MySql.Models;
using System;

namespace PreFlight_API.BLL.Mappings
{
    public class JobCategoryMapping : Profile
    {
        public JobCategoryMapping()
        {
            CreateMap<JobCategory, JobCategoryEntity>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(d => d.JobCategoryName, opt => opt.MapFrom(src => src.JobCategoryName));

            CreateMap<JobCategoryEntity, JobCategory>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id.ToString())))
                .ForMember(d => d.JobCategoryName, opt => opt.MapFrom(src => src.JobCategoryName));
                
        }
    }
}
