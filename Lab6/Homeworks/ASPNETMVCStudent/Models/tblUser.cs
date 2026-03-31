using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Add this

namespace ASPNETMVCStudent.Models;

[Table("tblUser")] // <--- Force C# to look for the exact lowercase name in SQL
public partial class TblUser
{
    [Key]
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? Email { get; set; }
}