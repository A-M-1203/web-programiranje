namespace Oktobar2025Grupa1.Requests;

public class UpdateSastojciHamburgeraRequest
{
    public required int Id { get; set; }
    public List<string> Sastojci { get; set; } = [];
}