using AutoFixture;
using PreFlight_API.BLL.Tests.Helpers;
using Xunit;
using PreFlight_API.DAL.MySql.Models;
using System;
using Newtonsoft.Json;
using PreFlight_API.BLL.Models;

namespace PreFlight_API.BLL.Tests
{
    public class LocationServiceTests
    {
        [Theory]
        [InlineData("Juneau", Country.America)]
        
        public void CreateLocation_Test(string name, Country country)
        {
            //Arrange
            var fixture = new Fixture();

            var location = Fixtures.LocationFixture(name, country);
            var mapper = Mapper.GetAutoMapper();
            var locationRepoMoq = LocationMoqs.LocationRepositoryMoq(mapper.Map<LocationEntity>(location));
            var locationSvc = new LocationService(mapper, locationRepoMoq.Object);

            //Act
            var newLocation = locationSvc.CreateLocationAsync(location).Result;

            //Assert
            var actual = JsonConvert.SerializeObject(location);
            var expected = JsonConvert.SerializeObject(newLocation);
            Assert.Equal(expected.Trim(), actual.Trim());
        }

        [Fact]
        public void GetLocation_Test()
        {
            //Arrange
            var fixture = new Fixture();

            var location = Fixtures.LocationFixture();
            var mapper = Mapper.GetAutoMapper();
            var locationRepoMoq = LocationMoqs.LocationReposirotyMoq(mapper.Map<LocationEntity>(location));
            var locationSvc = new LocationService(mapper, locationRepoMoq.Object);

            //Act
            var result = locationSvc.GetLocationAsync(fixture.Create<Guid>()).Result;

            //Assert
            var actual = JsonConvert.SerializeObject(location);
            var expected = JsonConvert.SerializeObject(result);
            Assert.Equal(expected.Trim(), actual.Trim());
        }
    }
}
