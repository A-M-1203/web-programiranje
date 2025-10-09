using Januar2025.Responses.AutomobilResponses;

namespace Januar2025.Responses.KorisnikResponses;

public class KorisnikIznajmljivanjeResponse
{
    public required int Id { get; set; }
    public required AutomobilBasicResponse Automobil { get; set; }
    public required int BrojDana { get; set; }
}