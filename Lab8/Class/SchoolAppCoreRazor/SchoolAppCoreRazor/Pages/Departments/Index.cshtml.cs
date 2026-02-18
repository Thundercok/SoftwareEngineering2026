using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly SchoolAppCoreRazor.Models.SchoolContext _context;

        public IndexModel(SchoolAppCoreRazor.Models.SchoolContext context)
        {
            _context = context;
        }

        public IList<Department> Department { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Department = await _context.Departments.ToListAsync();
        }
    }
}
