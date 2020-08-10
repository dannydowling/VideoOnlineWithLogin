using System;

namespace PreFlight_API.BLL.Models
{
    public class JobCategory 
    {
        public Guid Id { get; set; }
        public string JobCategoryName { get; set; }
    }
    

    public enum JobCategoryEnum
    {
        User = 0

    }
}

