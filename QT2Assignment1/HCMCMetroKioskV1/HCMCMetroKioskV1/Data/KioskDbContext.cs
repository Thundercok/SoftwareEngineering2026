using Microsoft.EntityFrameworkCore;
using HCMCMetroKioskV1.Models;

namespace HCMCMetroKioskV1.Data;

public class KioskDbContext : DbContext
{
    public KioskDbContext(DbContextOptions<KioskDbContext> options) : base(options) { }

    public DbSet<Station> Stations => Set<Station>();
    public DbSet<FareRule> FareRules => Set<FareRule>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
}