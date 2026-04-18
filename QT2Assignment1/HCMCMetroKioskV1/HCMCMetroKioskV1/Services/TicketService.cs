using HCMCMetroKioskV1.Data;
using HCMCMetroKioskV1.Models;
using Microsoft.EntityFrameworkCore;

namespace HCMCMetroKioskV1.Services;

public class TicketService
{
    private readonly KioskDbContext _db;
    private static int _counter = 0;

    public TicketService(KioskDbContext db) => _db = db;
    public async Task<int> GetFareCountAsync()
        => await _db.FareRules.CountAsync();
    public async Task<List<Station>> GetStationsAsync()
        => await _db.Stations
            .Where(s => s.IsActive)
            .OrderBy(s => s.StationOrder)
            .ToListAsync();

    public async Task<FareRule?> GetFareAsync(int fromOrder, int toOrder)
    {
        var fare = await _db.FareRules
            .FirstOrDefaultAsync(f => f.FromStationOrder == fromOrder
                                      && f.ToStationOrder   == toOrder);
        if (fare != null) return fare;

        // Fallback matrix while DB is empty
        var matrix = new int[14, 14]
        {
            {  0,  7,  7,  7,  7,  7,  7,  9, 10, 12, 14, 16, 18, 20 },
            {  7,  0,  7,  7,  7,  7,  7,  8, 10, 11, 13, 16, 17, 20 },
            {  7,  7,  0,  7,  7,  7,  7,  7,  9, 10, 12, 15, 16, 18 },
            {  7,  7,  7,  0,  7,  7,  7,  7,  7,  8, 10, 13, 14, 17 },
            {  7,  7,  7,  7,  0,  7,  7,  7,  7,  7,  9, 12, 13, 16 },
            {  7,  7,  7,  7,  7,  0,  7,  7,  7,  7,  8, 10, 12, 14 },
            {  7,  7,  7,  7,  7,  7,  0,  7,  7,  7,  7,  9, 11, 13 },
            {  9,  8,  7,  7,  7,  7,  7,  0,  7,  7,  7,  8,  9, 11 },
            { 10, 10,  9,  7,  7,  7,  7,  7,  0,  7,  7,  7,  8, 10 },
            { 12, 11, 10,  8,  7,  7,  7,  7,  7,  0,  7,  7,  7,  8 },
            { 14, 13, 12, 10,  9,  8,  7,  7,  7,  7,  0,  7,  7,  7 },
            { 16, 16, 15, 13, 12, 10,  9,  8,  7,  7,  7,  0,  7,  7 },
            { 18, 17, 16, 14, 13, 12, 11,  9,  8,  7,  7,  7,  0,  7 },
            { 20, 20, 18, 17, 16, 14, 13, 11, 10,  8,  7,  7,  7,  0 },
        };

        int f = fromOrder - 1;
        int t = toOrder   - 1;
        if (f < 0 || f >= 14 || t < 0 || t >= 14) return null;

        int cash = matrix[f, t] * 1000;
        return new FareRule
        {
            FromStationOrder = fromOrder,
            ToStationOrder   = toOrder,
            CashPrice        = cash,
            NonCashPrice     = cash - 1000
        };
    }
    

    public async Task<Ticket> CreateTicketAsync(
        int fromStationId, int toStationId, PaymentMethod method,
        bool isReturn = false, int quantity = 1)
    {
        var from = await _db.Stations.FindAsync(fromStationId)
                   ?? throw new Exception("Invalid from station");
        var to   = await _db.Stations.FindAsync(toStationId)
                   ?? throw new Exception("Invalid to station");

        var fare = await GetFareAsync(from.StationOrder, to.StationOrder)
                   ?? throw new Exception("No fare rule found");

        int totalPrice = fare.NonCashPrice * quantity * (isReturn ? 2 : 1);

        int seq = Interlocked.Increment(ref _counter);
        var ticket = new Ticket
        {
            TicketCode    = $"TKT-{DateTime.Now:yyyyMMdd}-{seq:D4}",
            Type          = TicketType.SingleTrip,
            FromStationId = fromStationId,
            ToStationId   = toStationId,
            Price         = totalPrice,
            PaymentMethod = method,
            Status        = TicketStatus.Pending,
            CreatedAt     = DateTime.Now,
            ValidFrom     = DateTime.Now,
            ValidUntil    = DateTime.Now.AddMinutes(90),
            IsReturn      = isReturn,
            Quantity      = quantity,
        };

        _db.Tickets.Add(ticket);
        await _db.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket?> GetTicketAsync(int id)
        => await _db.Tickets
            .Include(t => t.FromStation)
            .Include(t => t.ToStation)
            .FirstOrDefaultAsync(t => t.Id == id);

    public async Task<bool> ConfirmPaymentAsync(int ticketId)
    {
        var ticket = await _db.Tickets.FindAsync(ticketId);
        if (ticket == null) return false;

        ticket.Status        = TicketStatus.Paid;
        ticket.PaidAt        = DateTime.Now;
        ticket.TransactionId = $"TXN{Guid.NewGuid():N}".ToUpper()[..16];
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CancelTicketAsync(int ticketId)
    {
        var ticket = await _db.Tickets.FindAsync(ticketId);
        if (ticket == null) return false;

        ticket.Status = TicketStatus.Cancelled;
        await _db.SaveChangesAsync();
        return true;
    }
}