using System.Linq;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class AccountController : Controller
    {
        private Lab2DBEntities2 db = new Lab2DBEntities2();

        // GET: /Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = db.Users.FirstOrDefault(u =>
                u.UserName == username &&
                u.Password == password &&
                u.IsLocked == false);

            if (user != null)
            {
                Session["UserName"] = user.UserName;
                return RedirectToAction("Index", "Agents");
            }

            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}