namespace TicketVendorMachine.Web.Models
{
    public class TicketViewModel
    {
        public string OriginStation { get; set; } = "Ben Thanh (Zone 1)";
        public List<Station> AvailableStations { get; set; } = new();
        public string SelectedDestination { get; set; } = "";
        public decimal Fare { get; set; }
        public string PaymentMethod { get; set; } = "CreditCard";
        public string CardNumber { get; set; } = "";
        public string Status { get; set; } = "Ready";
        public string StatusColor { get; set; } = "#90CAF9";
        public Ticket? IssuedTicket { get; set; }
    }
}