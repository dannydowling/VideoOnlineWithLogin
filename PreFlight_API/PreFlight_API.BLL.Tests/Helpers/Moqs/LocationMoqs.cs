using AutoFixture;
using Moq;
using PreFlight_API.BLL.Contracts;
using PreFlight_API.BLL.Models;
using PreFlight_API.DAL.MySql.Contract;
using PreFlight_API.DAL.MySql.Models;
using System;

namespace PreFlight_API.BLL.Tests.Helpers
{
    public static class LocationMoqs
    {
        public static Mock<ILocationService> LocationServiceMoq()
        {
            var fixture = new Fixture();

            var moq = new Mock<ILocationService>(MockBehavior.Strict);
            moq.Setup(s => s.CreateLocationAsync(It.IsAny<Location>()))
              .ReturnsAsync(fixture.Build<Location>().Create());
            moq.Setup(s => s.GetLocationAsync(It.IsAny<Guid>()))
             .ReturnsAsync(fixture.Build<Location>().Create());
            moq.Setup(s => s.UpdateLocationAsync(It.IsAny<Location>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteLocationAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetLocationListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<Location>().CreateMany(20));

            return moq;
        }

        public static Mock<ILocationRepository> LocationRepositoryMoq(LocationEntity locationEntity)
        {
            var fixture = new Fixture();

            var moq = new Mock<ILocationRepository>(MockBehavior.Strict);
            moq.Setup(s => s.CreateLocationAsync(It.IsAny<LocationEntity>()))
              .ReturnsAsync(locationEntity);
            moq.Setup(s => s.GetLocationAsync(It.IsAny<Guid>()))
             .ReturnsAsync(carEntity);
            moq.Setup(s => s.UpdateLocationAsync(It.IsAny<LocationEntity>()))
              .ReturnsAsync(true);
            moq.Setup(s => s.DeleteLocationAsync(It.IsAny<Guid>()))
            .ReturnsAsync(true);
            moq.Setup(s => s.GetLocationListAsync(It.IsAny<int>(), It.IsAny<int>()))
             .ReturnsAsync(fixture.Build<LocationEntity>().CreateMany(20));

            return moq;
        }
    }
}
