using Microsoft.EntityFrameworkCore;
public class Exer1DbContext : DbContext {
    public Exer1DbContext(DbContextOptions<Exer1DbContext> options) : base(options) { }
    public DbSet<Customer> Customers { get; set; } // [cite: 188]
}