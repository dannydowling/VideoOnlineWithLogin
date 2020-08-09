using AutoMapper;
using PreFlight_API.BLL.Mappings;

namespace PreFlight_API.BLL.Tests.Helpers
{
    public static class Mapper
    {
        public static IMapper GetAutoMapper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EmployeeMapping());
                cfg.AddProfile(new LocationMapping());
                cfg.AddProfile(new UserMapping());
                cfg.AddProfile(new WeatherMapping());
                //cfg.AddProfile(new CarsMapping());

            });
            var mapper = mockMapper.CreateMapper();
            return mapper;
        }
    }
}
