using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace PreFlightAI.Shared.Users
{
    public class GameOffering
    {

        [Key]
        public int gameId { get; set; }

        [StringLength(150, ErrorMessage = "Please enter a truth.")]
        public string FirstTruth { get; set; }

        
        [StringLength(150, ErrorMessage = "Please enter a truth.")]
        public string SecondTruth { get; set; }

        
        [StringLength(150, ErrorMessage = "Please enter a Lie.")]
        public string Lie { get; set; }

    }
}
