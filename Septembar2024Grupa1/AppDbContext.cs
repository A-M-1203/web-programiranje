using Microsoft.EntityFrameworkCore;
using Septembar2024Grupa1.Models;

namespace Septembar2024Grupa1;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Stan> Stanovi { get; set; }
    public DbSet<Racun> Racuni { get; set; }
}