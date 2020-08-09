using System;
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

        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Range(0, 99999)]
        private int _rowVersion;
        public int RowVersion
        {
            get { return _rowVersion; }
            set { _rowVersion = value++; }
        }
    }
}
