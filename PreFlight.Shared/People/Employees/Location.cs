using System.ComponentModel.DataAnnotations;


namespace PreFlightAI.Shared.Employee
{
    public class Location
    {
        [Key]
        public int employeeLocationId { get; set; }
        public string employeeLocationName { get; set; }


    }    
}
