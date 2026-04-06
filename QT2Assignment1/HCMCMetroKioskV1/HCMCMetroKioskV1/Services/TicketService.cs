using HCMCMetroKioskV1.Data;
using HCMCMetroKioskV1.Models;
using Microsoft.EntityFrameworkCore;

namespace HCMCMetroKioskV1.Services;

public class TicketService
{
    private readonly KioskDbContext _db;
    private static int _counter = 0;

    public TicketService(KioskDbContext db) => _db = db;

    public async Task<List<Station>> GetStationsAsync()
        => await _db.Stations
            .Where(s => s.IsActive)
            .OrderBy(s => s.StationOrder)
            .ToListAsync();

    public async Task<FareRule?> GetFareAsync(int fromOrder, int toOrder)
        => await _db.FareRules
            .FirstOrDefaultAsync(f => f.FromStationOrder == fromOrder
                                   && f.ToStationOrder   == toOrder);

    public async Task<Ticket> CreateTicketAsync(
        int fromStationId, int toStationId, PaymentMethod method)
    {
        var from = await _db.Stations.FindAsync(fromStationId)
                   ?? throw new Exception("Invalid from station");
        var to   = await _db.Stations.FindAsync(toStationId)
                   ?? throw new Exception("Invalid to station");

        var fare = await GetFareAsync(from.StationOrder, to.StationOrder)
                   ?? throw new Exception("No fare rule found");

        int seq = Interlocked.Increment(ref _counter);
        var ticket = new Ticket
        {
            TicketCode     = $"TKT-{DateTime.Now:yyyyMMdd}-{seq:D4}",
            Type           = TicketType.SingleTrip,
            FromStationId  = fromStationId,
            ToStationId    = toStationId,
            Price          = fare.NonCashPrice,   // digital payment = non-cash
            PaymentMethod  = method,
            Status         = TicketStatus.Pending,
            CreatedAt      = DateTime.Now,
            ValidFrom      = DateTime.Now,
            ValidUntil     = DateTime.Now.AddMinutes(90),
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