using PreFlight.AI.Server.ModelsandRepositories.Employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreFlightAI.Shared
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

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

        public ICollection<Location> employeeLocations { get; set; } = new List<Location>();

        public Role Role { get; set; }

       
        public DateTime JoinedDate { get; set; }
        public DateTime? ExitDate { get; set; }



        public ICollection<Weather> employeeWeatherList { get; set; }

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
