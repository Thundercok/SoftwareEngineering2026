using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Instructors
{
    public class DetailsModel : PageModel
    {
        private readonly SchoolAppCoreRazor.Models.SchoolContext _context;

        public DetailsModel(SchoolAppCoreRazor.Models.SchoolContext context)
        {
            _context = context;
        }

        public Instructor Instructor { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instructor = await _context.Instructors.FirstOrDefaultAsync(m => m.InstructorID == id);

            if (instructor is not null)
            {
                Instructor = instructor;

                return Page();
            }

            return NotFound();
        }
    }
}
