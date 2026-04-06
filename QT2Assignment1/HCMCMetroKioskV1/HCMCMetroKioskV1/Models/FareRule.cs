namespace HCMCMetroKioskV1.Models;

public class FareRule
{
    public int Id { get; set; }
    public int FromStationOrder { get; set; }
    public int ToStationOrder { get; set; }
    public int CashPrice { get; set; }        // VND (thousands in table × 1000)
    public int NonCashPrice { get; set; }     // CashPrice - 1000
}