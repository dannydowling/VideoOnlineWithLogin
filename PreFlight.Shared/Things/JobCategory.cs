using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PreFlightAI.Shared.Things
{
    public class JobCategory
    {
        [Key]
        public int JobCategoryId { get; set; }
        public string JobCategoryName { get; set; }

    }
}
