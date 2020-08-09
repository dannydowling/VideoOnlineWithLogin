using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PreFlight_API.API.Models
{
    public class UserModel
    {        
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(45, MinimumLength = 1, ErrorMessage = "First name is too long.")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(45, MinimumLength = 1, ErrorMessage = "Last name is too long.")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(1000, ErrorMessage = "Comment length can't exceed 1000 characters.")]
        public string? Comment { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExitDate { get; set; }

        public ICollection<Weather> Weathers { get; set; }

        public JobCategory? JobCategory { get; set; }
        
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

    }
}
