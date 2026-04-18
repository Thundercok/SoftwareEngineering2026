using Microsoft.AspNetCore.Mvc;

namespace HCMCMetroKioskV1.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();

    // Error screens
    public IActionResult Error520() => View();
    public IActionResult Error5()   => View();
    public IActionResult Error408() => View();
}