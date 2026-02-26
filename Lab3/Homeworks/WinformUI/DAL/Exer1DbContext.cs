using Microsoft.EntityFrameworkCore;

namespace DAL;

public class Exer1DbContext : DbContext
{
    // Dòng constructor này phải viết liền mạch, không thừa dấu phẩy
    public Exer1DbContext(DbContextOptions<Exer1DbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<User> Users { get; set; }
}