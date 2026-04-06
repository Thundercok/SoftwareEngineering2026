namespace HCMCMetroKioskV1.Models;

public class Station
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string NameEn { get; set; }
    public int StationOrder { get; set; }   // 1 = Ben Thanh, 14 = Suoi Tien
    public bool IsUnderground { get; set; } // stations 1-3 are underground
    public bool IsActive { get; set; } = true;
}