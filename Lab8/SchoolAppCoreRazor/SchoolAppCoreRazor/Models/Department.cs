using System;
using System.Collections.Generic;

namespace SchoolAppCoreRazor.Models 
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

