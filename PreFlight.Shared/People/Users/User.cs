using System;
using System.ComponentModel.DataAnnotations;



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

        [Range(0, 99999)]
        private int _rowVersion;
        public int RowVersion
        {
            get { return _rowVersion; }
            set { _rowVersion = value++; }
        }
        public DateTime? JoinedDate { get; set; }
        public DateTime? ExitDate { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
