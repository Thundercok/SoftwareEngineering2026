using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC_Core.Data;
using WebMVC_Core.Models;
using Microsoft.AspNetCore.Http; // Required for ISession

namespace WebMVC_Core.Controllers
{
    public class AgentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        // In Core, we inject the database context through the constructor
        public AgentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ASP.NET Core uses HttpContext.Session instead of just Session[]
        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("UserName") != null;
        }

        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            return View(await _context.Agents.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (id == null) return NotFound();
            
            var agent = await _context.Agents.FindAsync(id);
            if (agent == null) return NotFound();
            
            return View(agent);
        }

        public IActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgentID,AgentName,Address")] Agent agent)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                _context.Add(agent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agent);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (id == null) return NotFound();

            var agent = await _context.Agents.FindAsync(id);
            if (agent == null) return NotFound();
            
            return View(agent);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgentID,AgentName,Address")] Agent agent)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (id != agent.AgentID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgentExists(agent.AgentID)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(agent);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (id == null) return NotFound();

            var agent = await _context.Agents.FirstOrDefaultAsync(m => m.AgentID == id);
            if (agent == null) return NotFound();

            return View(agent);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            var agent = await _context.Agents.FindAsync(id);
            if (agent != null)
            {
                _context.Agents.Remove(agent);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool AgentExists(int id)
        {
            return _context.Agents.Any(e => e.AgentID == id);
        }
    }
}