using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Pages.Models;
using WebApplication1.Pages.Models;


namespace SchoolAppCoreRazor.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext>options) : base(options) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
    }
}