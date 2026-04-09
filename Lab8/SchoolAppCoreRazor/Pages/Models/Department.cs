using System;
using System.Collections.Generic;
using WebApplication1.Pages.Models;

namespace SchoolAppCoreRazor.Pages.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public required string DepartmentName { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }

        public ICollection<Course>? Courses { get; set; }

    }
}