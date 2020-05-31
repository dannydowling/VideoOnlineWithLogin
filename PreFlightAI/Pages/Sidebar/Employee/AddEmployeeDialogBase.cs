﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using PreFlightAI.Shared;
using PreFlightAI.Server.Services;
using PreFlightAI.Shared.Employee;
using PreFlightAI.Shared.Places;
using PreFlightAI.Shared.Things;

namespace PreFlightAI.Server.Pages
{
    public class AddEmployeeDialogBase : ComponentBase
    {
        public bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        [Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }
        public Employee Employee { get; set; } = new Employee 
        { EmployeeId = 0, LocationId = 1, JobCategoryId = 1};

        [Inject]
        public IJobCategoryDataService JobCategoryDataService { get; set; }
        public List<Shared.Things.JobCategory> JobCategories { get; set; } = new List<Shared.Things.JobCategory>();
        internal int JobCategoryId = 0;


        public void Show()
        {
            GetJobCategoriesAsync();
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        public async void GetJobCategoriesAsync()
        {
            JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();
        }

        private void ResetDialog()
        {
            Employee = new Employee { EmployeeId = 0, LocationId = 1, JobCategoryId = 1};
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            await EmployeeDataService.AddEmployee(Employee);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
