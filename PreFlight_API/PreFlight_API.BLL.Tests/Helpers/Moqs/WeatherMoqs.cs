using AutoFixture;
using Moq;
using PreFlight_API.BLL.Contracts;
using PreFlight_API.BLL.Models;
using PreFlight_API.DAL.MySql.Contract;
using PreFlight_API.DAL.MySql.Models;
using System;

namespace PreFlight_API.BLL.Tests.Helpers
{
    public static class WeatherMoqs
    {
        public static Mock<IWeatherService> WeatherServiceMoq()
        {
            var fixture = new Fixture();

            var moq = new Mock<IWeatherService>(MockBehavior.Strict);
            moq.Setup(s => s.CreateWeatherAsync(It.IsAny<Weather>()))
              .ReturnsAsync(fixture.Build<Weather>().Create());
            moq.Setup(s => s.GetWeatherAsync(It.IsAny<Guid>()))
             .ReturnsAsync(fixture.Build<Weather>().Create());
            moq.Setup(s => s.UpdateWeatherAsync(It.IsAny<Weather>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteWeatherAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetWeatherListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<Weather>().CreateMany(20));

            return moq;
        }

        public static Mock<IWeatherRepository> WeatherRepositoryMoq(WeatherEntity weatherEntity)
        {
            var fixture = new Fixture();

            var moq = new Mock<IWeatherRepository>(MockBehavior.Strict);
            moq.Setup(s => s.CreateWeatherAsync(It.IsAny<WeatherEntity>()))
              .ReturnsAsync(weatherEntity);
            moq.Setup(s => s.GetWeatherAsync(It.IsAny<Guid>()))
             .ReturnsAsync(weatherEntity);
            moq.Setup(s => s.UpdateWeatherAsync(It.IsAny<WeatherEntity>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteWeatherAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetWeatherListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<WeatherEntity>().CreateMany(20));

            return moq;
        }
    }
}
