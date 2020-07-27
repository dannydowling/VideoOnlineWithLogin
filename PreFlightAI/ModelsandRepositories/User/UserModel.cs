using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PreFlightAI.Shared
{
    public class UserModel
    {
        [Key]
        public int UserID { get; set; }
        
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
        public byte[] RowVersion { get; set; }
        [DataType(DataType.Date)]
        public DateTime JoinedDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ExitDate { get; set; }

        public ICollection<Weather> Weathers { get; set; } = new List<Weather>();

        public JobCategory JobCategory { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
