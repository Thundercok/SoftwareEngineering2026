using Microsoft.EntityFrameworkCore;
namespace SchoolAppCoreRazor.Models
{
    public class SchoolContext : DbContext
    {
        
        public SchoolContext() { }
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SchoolDB;User Id=sa;Password=Somethingstupid1@;TrustServerCertificate=True");
            }
        }

    }
}