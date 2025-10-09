namespace Januar2025.Models;

public class Automobil
{
    public int Id { get; set; }
    public required string Model { get; set; }
    public required int Kilometraza { get; set; }
    public required int Godiste { get; set; }
    public required int BrojSedista { get; set; }
    public int CenaPoDanu { get; set; }
    public bool TrenutnoIznajmljen { get; set; } = false;
    public List<Iznajmljivanje> Iznajmljivanja { get; set; } = [];
}