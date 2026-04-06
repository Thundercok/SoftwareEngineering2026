namespace HCMCMetroKioskV1.Models;

public enum TicketType
{
    SingleTrip,
    OneDay,
    ThreeDay,
    Monthly
}

public enum PaymentMethod
{
    CreditCard,
    MoMo,
    VNPay,
    ZaloPay
}

public enum TicketStatus
{
    Pending,
    Paid,
    Cancelled,
    Expired
}

public class Ticket
{
    public int Id { get; set; }
    public required string TicketCode { get; set; }       // e.g. TKT-20250406-0001

    public TicketType Type { get; set; } = TicketType.SingleTrip;

    // Single trip fields (nullable for day/month passes)
    public int? FromStationId { get; set; }
    public Station? FromStation { get; set; }
    public int? ToStationId { get; set; }
    public Station? ToStation { get; set; }

    public int Price { get; set; }                        // VND
    public PaymentMethod PaymentMethod { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Pending;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? PaidAt { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidUntil { get; set; }

    public string? TransactionId { get; set; }
    public string MachineId { get; set; } = "TVM-BT-001";
    public string SessionToken { get; set; } = Guid.NewGuid().ToString("N");
    public string BarcodeData { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
}