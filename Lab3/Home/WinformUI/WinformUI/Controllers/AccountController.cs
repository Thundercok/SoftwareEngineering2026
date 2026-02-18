using Microsoft.AspNetCore.Mvc; // Quan trọng để hết lỗi Controller, IActionResult, HttpPost
using BLL; // Để nhận diện UserService
using DAL; // Để nhận diện model User nếu cần

namespace WinformUI.Controllers;

// Đảm bảo class kế thừa từ Controller
public class AccountController : Controller
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        if (_userService.Authenticate(username, password))
        {
            // Đăng nhập thành công, chuyển hướng sang Main Form (Exercise 3)
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Sai tài khoản hoặc mật khẩu!";
        return View();
    }
}