using HCMCMetroKioskV1.Models;
using HCMCMetroKioskV1.Services;
using Microsoft.AspNetCore.Mvc;
using HCMCMetroKioskV1.Data; 
using Microsoft.EntityFrameworkCore;

namespace HCMCMetroKioskV1.Controllers;

public class TicketController : Controller
{
    // 1. Khai báo cả 2 private readonly fields ở đây
    private readonly TicketService _svc;
    private readonly KioskDbContext _db;

    // 2. Inject cả 2 vào constructor
    public TicketController(TicketService svc, KioskDbContext db)
    {
        _svc = svc;
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> Buy()
    {
        var stations = await _svc.GetStationsAsync();
        return View(stations);
    }

    [HttpGet]
    public async Task<IActionResult> GetFare(int fromId, int toId)
    {
        // fromId and toId ARE the station IDs which equal StationOrder in our seed
        var fare = await _svc.GetFareAsync(fromId, toId);
        if (fare == null)
            return Json(new { cashPrice = 0, nonCashPrice = 0, display = "0 ₫" });

        return Json(new {
            cashPrice    = fare.CashPrice,
            nonCashPrice = fare.NonCashPrice,
            display      = $"{fare.NonCashPrice:#,##0} ₫"
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> Confirm(
        int toStationId, string paymentMethod,
        bool isReturn = false, int quantity = 1)
    {
        var method = Enum.Parse<PaymentMethod>(paymentMethod);
        var ticket = await _svc.CreateTicketAsync(1, toStationId, method, isReturn, quantity);
        return RedirectToAction("PayQR", new { id = ticket.Id });
    }

    [HttpGet]
    public IActionResult Debug()
    {
        // Bây giờ _db đã tồn tại, hàm này sẽ hoạt động bình thường
        var fareCount = _db.FareRules.Count();
        var stationCount = _db.Stations.Count();
        return Content($"Stations: {stationCount}, Fares: {fareCount}");
    }
    
    [HttpGet]
    public async Task<IActionResult> PayQR(int id)
    {
        var ticket = await _svc.GetTicketAsync(id);
        if (ticket == null) return RedirectToAction("Buy");
        return View(ticket);
    }

    [HttpGet]
    public async Task<IActionResult> PayCard(int id)
    {
        var ticket = await _svc.GetTicketAsync(id);
        if (ticket == null) return RedirectToAction("Buy");
        return View(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> ProcessPayment(int id)
    {
        var ok = await _svc.ConfirmPaymentAsync(id);
        if (!ok) return RedirectToAction("PayFail", new { id });
        return RedirectToAction("PaySuccess", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> Cancel(int id)
    {
        await _svc.CancelTicketAsync(id);
        return RedirectToAction("Buy");
    }

    [HttpGet]
    public async Task<IActionResult> CheckStatus(string ticketCode)
    {
        var ticket = await _db.Tickets.FirstOrDefaultAsync(t => t.TicketCode == ticketCode);
        if (ticket == null) return Json(new { status = "NotFound" });
        return Json(new { status = ticket.Status.ToString() });
    }

    [HttpGet]
    public async Task<IActionResult> Success(string code)
    {
        var ticket = await _db.Tickets
            .Include(t => t.FromStation)
            .Include(t => t.ToStation)
            .FirstOrDefaultAsync(t => t.TicketCode == code);
        if (ticket == null) return RedirectToAction("Buy");
        return View(ticket);
    }

    [HttpGet]
    public IActionResult PayFail(int id) => View(id);
}