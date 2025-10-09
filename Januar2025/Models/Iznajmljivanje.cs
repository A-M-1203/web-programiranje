namespace Januar2025.Models;

public class Iznajmljivanje
{
    public int Id { get; set; }
    public Automobil? Automobil { get; set; }
    public Korisnik? Korisnik { get; set; }
    public int BrojDana { get; set; }
}