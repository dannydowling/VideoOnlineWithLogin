
using System.ComponentModel.DataAnnotations;


namespace PreFlightAI.Shared.Things
{
    public class JobCategory
    {
        [Key]        
        public int JobCategoryId { get; set; }
        public string JobCategoryName { get; set; }

    }
}
