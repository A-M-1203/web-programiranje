using Microsoft.EntityFrameworkCore;
using Oktobar2025Grupa1.Models;

namespace Oktobar2025Grupa1;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Prodavnica> Prodavnice { get; set; }
    public DbSet<Hamburger> Hamburgeri { get; set; }
    public DbSet<Sastojak> Sastojci { get; set; }
}