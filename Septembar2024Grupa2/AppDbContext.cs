using Microsoft.EntityFrameworkCore;
using Septembar2024Grupa2.Models;

namespace Septembar2024Grupa2;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Proizvod> Proizvodi { get; set; }
    public DbSet<Kupovina> Kupovine { get; set; }
    public DbSet<ProizvodKupovina> ProizvodiKupovine { get; set; }
}