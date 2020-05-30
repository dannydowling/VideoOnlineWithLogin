using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PreFlightAI.Shared.Employee
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }
        public string Name { get; set; }


    }    
}
