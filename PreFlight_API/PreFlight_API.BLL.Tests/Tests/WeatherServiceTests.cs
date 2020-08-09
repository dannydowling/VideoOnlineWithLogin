using AutoFixture;
using PreFlight_API.BLL.Tests.Helpers;
using Xunit;
using PreFlight_API.DAL.MySql.Models;
using System;
using Newtonsoft.Json;
using PreFlight_API.BLL.Models;

namespace PreFlight_API.BLL.Tests
{
    public class WeatherServiceTests
    {
        [Theory]
      
        public void CreateWeather_Test(double airPressure, double temperature, double weightValue)
        {
            //Arrange
            var fixture = new Fixture();

            var weather = Fixtures.WeatherFixture(airPressure, temperature, weightValue);
            var mapper = Mapper.GetAutoMapper();
            var weatherRepoMoq = WeatherMoqs.WeatherRepositoryMoq(mapper.Map<WeatherEntity>(weather));
            var weatherSvc = new WeatherService(mapper, weatherRepoMoq.Object);

            //Act
            var newWeather = weatherSvc.CreateWeatherAsync(weather).Result;

            //Assert
            var actual = JsonConvert.SerializeObject(weather);
            var expected = JsonConvert.SerializeObject(newWeather);
            Assert.Equal(expected.Trim(), actual.Trim());
        }

        [Fact]
        public void GetWeather_Test()
        {
            //Arrange
            var fixture = new Fixture();

            var weather = Fixtures.WeatherFixture();
            var mapper = Mapper.GetAutoMapper();
            var weatherRepoMoq = WeatherMoqs.WeatherRepositoryMoq(mapper.Map<WeatherEntity>(weather));
            var weatherSvc = new WeatherService(mapper, weatherRepoMoq.Object);

            //Act
            var result = weatherSvc.GetWeatherAsync(fixture.Create<Guid>()).Result;

            //Assert
            var actual = JsonConvert.SerializeObject(weather);
            var expected = JsonConvert.SerializeObject(result);
            Assert.Equal(expected.Trim(), actual.Trim());
        }
    }
}
