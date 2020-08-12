using System;
using PreFlight_API.BLL;

namespace PreFlight_API.BLL.Models
{
    public class JobCategory 
    {
        public Guid Id { get; set; }

        private string _jobCategoryName;

        public string JobCategoryName
        {
            // get the name from the enum, whatever's in the backing field, 
            // of the type, the string associated with the enum...

            get { return EnumHelpers.GetName<JobCategoryEnum>(_jobCategoryName.GetType(), _jobCategoryName); }
            set { _jobCategoryName = value; }
        }
    }
    

    public enum JobCategoryEnum
    {
        User = 0

    }
}

