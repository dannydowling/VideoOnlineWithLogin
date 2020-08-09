namespace PreFlight_API.DAL.MySql
{
    public class PreFlightMySqlRepositoryOption
    {
        public string UserDbConnectionString { get; set; }
        public string WeatherDbConnectionString { get; set; }
        public string EmployeeDbConnectionString { get; set; }
        public string LocationDbConnectionString { get; set; }
    }
}
