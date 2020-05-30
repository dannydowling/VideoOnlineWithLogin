using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace PreFlightAI.Shared.Users
{
    public class typedUser
    {
        [Key]
        public int userId { get; set; }
        
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

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public DateTime? JoinedDate { get; set; }
        public DateTime? ExitDate { get; set; }

        public int gameId { get; set; }
        public GameOffering Game { get; set; } // 2truths1lie game currently set for this user.

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
