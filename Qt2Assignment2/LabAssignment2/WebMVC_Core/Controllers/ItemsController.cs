using Microsoft.AspNetCore.Mvc;
using WebMVC_Core.Data;
using WebMVC_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace WebMVC_Core.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index() => View(await _context.Items.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}