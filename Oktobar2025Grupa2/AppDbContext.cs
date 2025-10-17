using Microsoft.EntityFrameworkCore;
using Oktobar2025Grupa2.Models;

namespace Oktobar2025Grupa2;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<ProducentskaKuca> ProducentskeKuce { get; set; }
    public DbSet<Film> Filmovi { get; set; }
}