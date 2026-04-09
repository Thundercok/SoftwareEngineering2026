using System;
using System.Collections.Generic;

namespace WebApplication1.Pages.Models
{
    public class Depatment
    {
        public int DepartmentID { get; set; }
        public required string DepartmentName { get; set; }
        public decimal Budget { get; set; }
        public DateTime StartDate { get; set; }

        public ICollection<Course>? Courses { get; set; }

    }
}