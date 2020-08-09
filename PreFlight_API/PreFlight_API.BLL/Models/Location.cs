using System;

namespace PreFlight_API.BLL.Models
{
    public class Location
    {
       
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public Country Country { get; set; }

    }
}
