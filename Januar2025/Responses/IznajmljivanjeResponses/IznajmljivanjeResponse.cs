using Januar2025.Responses.AutomobilResponses;
using Januar2025.Responses.KorisnikResponses;

namespace Januar2025.Responses.IznajmljivanjeResponses;

public class IznajmljivanjeResponse
{
    public required int Id { get; set; }
    public required AutomobilBasicResponse Automobil { get; set; }
    public required KorisnikBasicResponse Korisnik { get; set; }
    public required int BrojDana { get; set; }
}