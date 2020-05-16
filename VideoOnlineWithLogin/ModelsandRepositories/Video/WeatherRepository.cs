using PreFlight.DataContexts;
using PreFlight.Interfaces;
using PreFlight.Types;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PreFlight.Repositories
{
    public class WeatherRepository : IWeather
    {
        private DensityContext densityContext;
        private CompanyContext companyContext;
        public WeatherRepository()
        {
            this.densityContext = new DensityContext();
            this.companyContext = new CompanyContext();
        }
        public int UpdateWeatherValues(int multiplier)
        {
            return densityContext.Database.ExecuteSqlCommand("UPDATE WeatherTable  = Weather * {0}", multiplier);
        }

        public Dictionary<Location, Weather> GetCompanyWeather(string companyName)
        {
            var company = companyContext.Companies.First(n => n.Name == companyName);

            Dictionary<Location, Weather> calculatedWeather = new Dictionary<Location, Weather>();

            foreach (var location in company.locations)
            { calculatedWeather.Add(location, location.Weather); }

            return calculatedWeather;
        }

        public IEnumerable<Weather> GetWeathers()
        {
            return densityContext.WeatherTable.ToList();
        }

        public Weather GetWeatherByID(int? id)
        {
            return densityContext.WeatherTable.Find(id);
        }

        public void InsertWeather(Weather weather)
        {
            densityContext.WeatherTable.Add(weather);
        }

        public void DeleteWeather(int weatherId)
        {
            Weather weather = densityContext.WeatherTable.Find(weatherId);
            densityContext.WeatherTable.Remove(weather);
        }

        public void UpdateWeather(Weather weather)
        {
            densityContext.Entry(weather).State = EntityState.Modified;
        }

        public void Save()
        {
            densityContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    densityContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}