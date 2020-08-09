using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreFlight_API.DAL.MySql.Models

{
    public class UserModelEntity
    {        
        public string Id { get; set; }
        
        [Required]
        [StringLength(50, ErrorMessage = "First name is too long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name is too long.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(1000, ErrorMessage = "Comment length can't exceed 1000 characters.")]
        public string Comment { get; set; }

        [DataType(DataType.Date)]
        public DateTime RowVersion { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ExitDate { get; set; }

        public ICollection<WeatherEntity> Weathers { get; set; }

       
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
