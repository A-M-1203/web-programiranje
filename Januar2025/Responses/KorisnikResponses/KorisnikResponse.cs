namespace Januar2025.Responses.KorisnikResponses;

public class KorisnikResponse
{
    public int Id { get; set; }
    public required string Ime { get; set; }
    public required string Prezime { get; set; }
    public required string Jmbg { get; set; }
    public required string BrojVozacke { get; set; }
    public List<KorisnikIznajmljivanjeResponse> Iznajmljivanja { get; set; } = [];
}