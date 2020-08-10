using Org.BouncyCastle.Bcpg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreFlight_API.DAL.MySql.Models
{
    public class EmployeeEntity
    {
        
        public string Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [StringLength(50, ErrorMessage = "First name is too long.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Last name is too long.")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        

        public int JobCategoryId { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public ICollection<LocationEntity> employeeLocations { get; set; }

       
       
        public DateTime JoinedDate { get; set; }
        public DateTime? ExitDate { get; set; }



        public ICollection<WeatherEntity> employeeWeatherList { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }


      [DataType(DataType.DateTime)]
      public DateTime RowVersion { get; set; }
    }
}
