namespace Januar2025.Responses.AutomobilResponses;

public class AutomobilResponse
{
    public int Id { get; set; }
    public required string Model { get; set; }
    public required int Kilometraza { get; set; }
    public required int Godiste { get; set; }
    public required int BrojSedista { get; set; }
    public required int CenaPoDanu { get; set; }
    public required bool TrenutnoIznajmljen { get; set; } = false;
    public List<AutomobilIznajmljivanjeResponse> Iznajmljivanja { get; set; } = [];
}