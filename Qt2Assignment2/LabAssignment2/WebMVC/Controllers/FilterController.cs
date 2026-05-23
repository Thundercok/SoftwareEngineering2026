using System.Linq;
using System.Web.Mvc;
using WebMVC; // Thêm dòng này để máy hiểu các Model

namespace WebMVC.Controllers
{
    public class FilterController : Controller
    {
        // Sử dụng đúng "người quản kho" của bạn
        private Lab2DBEntities2 db = new Lab2DBEntities2();

        // Thêm khóa bảo mật
        private bool IsLoggedIn() => Session["UserName"] != null;

        public ActionResult Index()
        {
            // Bắt buộc đăng nhập
            if (!IsLoggedIn()) return RedirectToAction("Login", "Account");

            // 1. Thống kê: Top 10 Sản phẩm bán chạy nhất
            ViewBag.BestItems = db.OrderDetails
                .GroupBy(od => od.Item.ItemName)
                .Select(g => new {
                    ItemName = g.Key,
                    TongSL = g.Sum(x => x.Quantity),
                    DoanhThu = g.Sum(x => x.Quantity * x.UnitAmount)
                })
                .OrderByDescending(x => x.TongSL)
                .Take(10)
                .ToList();

            // 2. Thống kê: Các Đại lý mua nhiều nhất
            ViewBag.TopAgents = db.Orders
                .GroupBy(o => o.Agent.AgentName)
                .Select(g => new {
                    AgentName = g.Key,
                    SoDon = g.Count(),
                    // Dùng (decimal?) để đề phòng trường hợp Đại lý chưa mua món nào gây lỗi
                    TongTien = g.SelectMany(o => o.OrderDetails)
                                 .Sum(od => (decimal?)(od.Quantity * od.UnitAmount)) ?? 0
                })
                .OrderByDescending(x => x.TongTien)
                .ToList();

            return View();
        }
    }
}