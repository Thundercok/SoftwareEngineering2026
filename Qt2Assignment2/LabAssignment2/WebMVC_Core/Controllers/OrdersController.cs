using Microsoft.AspNetCore.Mvc;
using WebMVC_Core.Data;
using WebMVC_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace WebMVC_Core.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context) => _context = context;

        public async Task<IActionResult> Index() 
        {
            var orders = await _context.Orders.Include(o => o.Agent).ToListAsync();
            return View(orders);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int agentId, List<OrderDetail> details)
        {
            var order = new Order
            {
                AgentID = agentId,
                OrderDate = DateTime.Now,
                OrderDetails = details
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}