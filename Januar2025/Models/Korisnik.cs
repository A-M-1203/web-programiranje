namespace Januar2025.Models;

public class Korisnik
{
    public int Id { get; set; }
    public required string Ime { get; set; }
    public required string Prezime { get; set; }
    public required string Jmbg { get; set; }
    public required string BrojVozacke { get; set; }
    public List<Iznajmljivanje> Iznajmljivanja { get; set; } = [];
}