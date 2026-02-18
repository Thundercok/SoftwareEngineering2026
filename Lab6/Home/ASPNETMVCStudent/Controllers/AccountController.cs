using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ASPNETMVCStudent.Models;

namespace ASPNETMVCStudent.Controllers
{
    public class AccountController : Controller
    {
        private readonly SchoolDbContext _context;

        public AccountController(SchoolDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            // 1. Basic validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "Please enter both username and password.";
                return View();
            }

            try 
            {
                // 2. FIXED: Added the FirstOrDefaultAsync query to actually fetch the user
                var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);

                if (user != null)
                {
                    // 3. Claims setup
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Username ?? "User"),
                        new Claim(ClaimTypes.Email, user.Email ?? "") 
                    };

                    // Using "CookieAuth" to match your Program.cs config
                    var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

                    var authProperties = new AuthenticationProperties {
                        IsPersistent = true 
                    };

                    await HttpContext.SignInAsync("CookieAuth", 
                        new ClaimsPrincipal(claimsIdentity), 
                        authProperties);

                    return RedirectToAction("Index", "Students");
                }
                else
                {
                    ViewBag.Error = "Invalid username or password. Please try again.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Detailed error for your console, friendly error for user
                Console.WriteLine($"Login Error: {ex.Message}");
                ViewBag.Error = "Database connection failed. Verify Docker SQL is running.";
                return View();
            }
        }

        // FIXED: Using "CookieAuth" here as well for consistency
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Login");
        }
    }
}