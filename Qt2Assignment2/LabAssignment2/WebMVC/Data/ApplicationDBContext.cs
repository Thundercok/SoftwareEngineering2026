using Microsoft.EntityFrameworkCore; // This will work after the NuGet install
using WebMVC.Models; // This tells the context where your Item/Agent/User classes are

namespace WebMVC.Data 
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {
        }

        public DbSet<Item> Items { get; set; } [cite: 4]
        public DbSet<Agent> Agents { get; set; } [cite: 5]
        public DbSet<Order> Orders { get; set; } [cite: 6]
        public DbSet<OrderDetail> OrderDetails { get; set; } [cite: 7]
        public DbSet<User> Users { get; set; } [cite: 7]
    }
}