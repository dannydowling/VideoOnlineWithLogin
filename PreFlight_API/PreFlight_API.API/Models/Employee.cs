using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreFlight_API.API.Models
{
    public class Employee
    {       
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "First name is too long.")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last name is too long.")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        

        public int JobCategoryId { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public ICollection<Location> employeeLocations { get; set; }

             
        public DateTime JoinedDate { get; set; }
        public DateTime? ExitDate { get; set; }



        public ICollection<Weather> employeeWeatherList { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
