using System;
using System.Collections.Generic;

namespace StudentsApp.Models;

public partial class tblStudents
{
    public int StudentId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly EnrollmentDate { get; set; }
}
