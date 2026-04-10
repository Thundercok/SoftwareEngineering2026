using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore; // Required for ToListAsync
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly SchoolAppCoreRazor.Models.SchoolContext _context;

        public CreateModel(SchoolAppCoreRazor.Models.SchoolContext context)
        {
            _context = context;
        }

        // This was likely missing - the HTML needs this for asp-for="Course.Title" etc.
        [BindProperty]
        public Course Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var departments = await _context.Departments.ToListAsync();

            if (departments == null || !departments.Any())
            {
                ViewData["DepartmentID"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            }
            else
            {
                // Your custom display text logic
                ViewData["DepartmentID"] = new SelectList(
                    departments.Select(d => new
                    {
                        d.DepartmentID,
                        DisplayText = $"{d.DepartmentID} - {d.DepartmentName}"
                    }),
                    "DepartmentID",
                    "DisplayText"
                );
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Courses == null || Course == null)
            {
                return Page();
            }

            _context.Courses.Add(Course);
            await _context.Set<Course>().AddAsync(Course); // Or _context.Courses.Add(Course)
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}