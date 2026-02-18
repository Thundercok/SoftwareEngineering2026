using System;
using System.Collections.Generic;

namespace ASPNETMVCStudent.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Budget { get; set; }

    public DateOnly StartDate { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
