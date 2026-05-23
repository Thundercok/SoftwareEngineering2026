using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using WebMVC; 

namespace WebMVC.Controllers
{
    public class OrdersController : Controller
    {
        private Lab2DBEntities2 db = new Lab2DBEntities2();

        private bool IsLoggedIn() => Session["UserName"] != null;

        public ActionResult Index()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            var orders = db.Orders
                           .Include(o => o.Agent)
                           .OrderByDescending(o => o.OrderID)
                           .ToList();
            return View(orders);
        }

        public ActionResult Details(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var order = db.Orders
                          .Include(o => o.Agent)
                          .Include(o => o.OrderDetails.Select(od => od.Item))
                          .FirstOrDefault(o => o.OrderID == id);

            if (order == null) return HttpNotFound();

            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName");
            return View(order);
        }

        public ActionResult Create()
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "AgentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = order.OrderID });
            }

            ViewBag.AgentID = new SelectList(db.Agents, "AgentID", "AgentName", order.AgentID);
            return View(order);
        }

        [HttpPost]
        public ActionResult AddDetail(int orderId, int itemId, int quantity, decimal unitAmount)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            var detail = new OrderDetail
            {
                OrderID = orderId,
                ItemID = itemId,
                Quantity = quantity,
                UnitAmount = unitAmount
            };

            db.OrderDetails.Add(detail);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = orderId });
        }

        public ActionResult Delete(int? id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var order = db.Orders.Find(id);
            if (order == null) return HttpNotFound();
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            var details = db.OrderDetails.Where(od => od.OrderID == id).ToList();
            db.OrderDetails.RemoveRange(details);

            var order = db.Orders.Find(id);
            if (order != null) db.Orders.Remove(order);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}