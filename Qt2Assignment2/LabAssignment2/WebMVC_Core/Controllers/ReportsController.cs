using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC_Core.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC_Core.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ReportsController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> BestItems()
        {
            // Requirement: Filter best items (most quantity sold)
            var bestItems = await _context.OrderDetails
                .Include(od => od.Item)
                .GroupBy(od => od.Item.ItemName)
                .Select(g => new { Name = g.Key, TotalSold = g.Sum(x => x.Quantity) })
                .OrderByDescending(x => x.TotalSold)
                .ToListAsync();

            return View(bestItems);
        }
    }
}