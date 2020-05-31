using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PreFlightAI.Shared;
using PreFlightAI.Server.Services;
using PreFlightAI.Shared.Employee;

namespace PreFlightAI.Server.Pages
{
    public class EmployeeDetailBase : ComponentBase
    {
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        [Inject]
        public IJobCategoryDataService JobCategoryDataService{ get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        protected string jobCategoryName = string.Empty;
       
        public Employee Employee { get; set; } = new Employee();

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
            
            jobCategoryName = (await JobCategoryDataService.GetJobCategoryById(Employee.JobCategoryId)).JobCategoryName;
        }
    }
}
