using Oktobar2025Grupa2.Models;

namespace Oktobar2025Grupa2.Responses;

public class ProducentskaKucaResponse
{
    public required int Id { get; set; }
    public required string Naziv { get; set; }
    public List<Film> Filmovi { get; set; } = [];
}