// Data/EsportContext.cs
using Microsoft.EntityFrameworkCore;

public class EsportContext : DbContext
{
    public EsportContext(DbContextOptions<EsportContext> options) : base(options) { }

    public DbSet<Tournament> Tournaments { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
}
