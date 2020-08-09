using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreFlight_API.DAL.MySql.Models
{
    public class LocationEntity
    {   
        public string Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public CountryEntity Country { get; set; }

    }
}
