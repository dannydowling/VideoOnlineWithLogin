using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PreFlightAI.Server.Services;
using PreFlight.AI.Server.Pages.SideBar.Employee;
using PreFlightAI.Shared.Employee;


namespace PreFlightAI.Server.Pages
{
    public class EmployeeOverviewBase: ComponentBase
    {
        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }

        [Inject]
        public ILocationDataService LocationDataService { get; set; }

        public List<Employee> Employees { get; set; }
        public List<Shared.Places.Location> employeeLocations { get; set; }

        protected AddEmployeeDialog AddEmployeeDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            employeeLocations = (await LocationDataService.GetAllLocations()).ToList();
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
        }

        public async void AddEmployeeDialog_OnDialogClose()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
            StateHasChanged();
        }

        protected void QuickAddEmployee()
        {
            AddEmployeeDialog.Show();
        }
    }
}
