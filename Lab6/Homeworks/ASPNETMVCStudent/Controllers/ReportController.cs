using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ASPNETMVCStudent.Models;

namespace ASPNETMVCStudent.Controllers
{
    [Authorize] // Keep it secure!
    public class ReportController : Controller
    {
        private readonly SchoolDbContext _context;

        public ReportController(SchoolDbContext context)
        {
            _context = context;
        }

        // Handles both the initial load and the form submission
        public async Task<IActionResult> Index(int? selectedCourseId)
        {
            // 1. Get all courses for the Dropdown
            var courses = await _context.Courses.ToListAsync();
            
            var viewModel = new CourseReportViewModel
            {
                // Make sure "CourseId" and "Title" match your exact property names in Course.cs
                Courses = new SelectList(courses, "CourseId", "Title") 
            };

            // 2. If a user picked a course, fetch the students
            if (selectedCourseId.HasValue)
            {
                viewModel.SelectedCourseId = selectedCourseId.Value;
                viewModel.StudentsInCourse = await _context.Enrollments
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .Where(e => e.CourseId == selectedCourseId.Value)
                    .ToListAsync();
            }

            return View(viewModel);
        }
    }
}