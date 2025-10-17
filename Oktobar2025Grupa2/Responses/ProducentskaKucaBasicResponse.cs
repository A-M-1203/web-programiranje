namespace Oktobar2025Grupa2.Responses;

public class ProducentskaKucaBasicResponse
{
    public required string Naziv { get; set; }
    public List<FilmBasicResponse> Filmovi { get; set; } = [];
}