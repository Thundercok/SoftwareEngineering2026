using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPNETMVCStudent.Models
{
    public class CourseReportViewModel
    {
        public int SelectedCourseId { get; set; }
        public SelectList? Courses { get; set; }
        
        // This will hold the filtered students
        public List<Enrollment>? StudentsInCourse { get; set; } 
    }
}