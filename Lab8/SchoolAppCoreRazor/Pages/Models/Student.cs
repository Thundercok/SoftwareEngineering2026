using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Pages.Models;

namespace SchoolAppCoreRazor.Pages.Models{
    public class Student : PageModel
    {
    public int StudentID { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public DateTime EnrollmentDate { get; set; }
    
    public ICollection<Enrollment>? Enrollments { get; set; }
    }
}
