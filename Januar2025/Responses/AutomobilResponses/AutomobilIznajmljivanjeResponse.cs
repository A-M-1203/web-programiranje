using Januar2025.Responses.KorisnikResponses;

namespace Januar2025.Responses.AutomobilResponses;

public class AutomobilIznajmljivanjeResponse
{
    public required int Id { get; set; }
    public required KorisnikBasicResponse Korisnik { get; set; }
    public required int BrojDana { get; set; }
}