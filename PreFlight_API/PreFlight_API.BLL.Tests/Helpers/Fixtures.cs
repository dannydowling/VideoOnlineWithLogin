using AutoFixture;
using PreFlight_API.BLL.Models;

namespace PreFlight_API.BLL.Tests.Helpers
{
    public static class Fixtures
    {
        public static Employee EmployeeFixture(string name = null, JobCategory jobType = 0)
        {
            var fixture = new Fixture();

            var employee = fixture.Build<Employee>();

            if (!string.IsNullOrEmpty(Name))
            {
                employee.With(s => s.Name, name);
            }

            if (jobType > 0)
            {
                employee.With(s => s.JobCategory, jobType);
            }

            return employee.Create();
        }

        public static Location LocationFixture(string name = null, Country country = 0)
        {
            var fixture = new Fixture();

            var location = fixture.Build<Location>();

            if (!string.IsNullOrEmpty(name))
            {
                location.With(s => s.Name, name);
            }

            if (country > 0)
            {
                location.With(s => s.Country, country);
            }

            return location.Create();
        }
    }
}
