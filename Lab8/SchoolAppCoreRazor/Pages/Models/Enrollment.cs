using SchoolAppCoreRazor.Pages.Models;
namespace WebApplication1.Pages.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }
        public int CourseID { get; set; }
        public int StudentID { get; set; }
        public Course? Course { get; set; }
        public Student? Student { get; set; }
    }
}