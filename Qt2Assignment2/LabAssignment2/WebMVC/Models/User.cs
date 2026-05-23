namespace WebMVC.Models
{
    public class User
    {
        public int UserID { get; set; } // Primary Key 
        public string UserName { get; set; } [cite: 7]
        public string Email { get; set; } [cite: 1]
        public string Password { get; set; } [cite: 7]
        public bool Lock { get; set; } [cite: 7]
    }
}