using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oktobar2025Grupa2.Models;

public class ProducentskaKuca
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Naziv producentske kuce je obavezno polje")]
    [MinLength(1, ErrorMessage = "Minimalna duzina naziva producentske kuce je 1 karaktera")]
    [MaxLength(30, ErrorMessage = "Maksimalna duzina naziva producentske kuce je 30 karaktera")]
    public required string Naziv { get; set; }

    [JsonIgnore]
    public List<Film> Filmovi { get; set; } = [];
}