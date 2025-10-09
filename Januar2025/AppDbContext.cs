using Januar2025.Models;
using Microsoft.EntityFrameworkCore;

namespace Januar2025;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Automobil> Automobili { get; set; }
    public DbSet<Korisnik> Korisnici { get; set; }
    public DbSet<Iznajmljivanje> Iznajmljivanja { get; set; }
}