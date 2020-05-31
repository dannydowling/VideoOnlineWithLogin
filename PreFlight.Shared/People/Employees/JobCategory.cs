﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PreFlightAI.Shared.Employee
{
    public class EmployeeJobCategory
    {
        [ForeignKey("JobCategoryId")]
        public int JobCategoryId { get; set; }
        public string JobCategoryName { get; set; }

    }
}
