using PreFlightAI.Shared.Places;
using PreFlightAI.Shared.Things;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace PreFlightAI.Shared.Employee
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name is too long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name is too long.")]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }

       
        public int LocationId { get; set; }
        public Location DeviceLocation { get; set; }

        public int JobCategoryId { get; set; }
        public JobCategory EmployeePosition { get; set; }

        public string PhoneNumber { get; set; }

        [StringLength(1000, ErrorMessage = "Comment length can't exceed 1000 characters.")]
        public string Comment { get; set; }
        public DateTime? JoinedDate { get; set; }
        public DateTime? ExitDate { get; set; }

        [Range(0, 99999)]
        private int _rowVersion;
        public int RowVersion
        {
            get { return _rowVersion; }
            set { _rowVersion = value++; }
        }


        [DataType(DataType.Password)]
        public string Password { get; set; }
              
    }
}
