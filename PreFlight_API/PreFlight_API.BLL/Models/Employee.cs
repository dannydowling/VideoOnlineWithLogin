using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PreFlight_API.BLL.Models
{
    public class Employee : UserModel
    {   
       
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }        

        public int JobCategoryId { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public ICollection<Location> employeeLocations { get; set; }
        
        public ICollection<Weather> overrideWeatherList { get; set; }
      
    }
}
