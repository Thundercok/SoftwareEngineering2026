namespace SchoolAppCoreRazor.Pages.Models;

public class ErrorViewModel
{
    public string? RequestID { get; set; }
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestID);
}