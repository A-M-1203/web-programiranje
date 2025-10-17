using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oktobar2025Grupa2.Models;

public class Film
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Naziv filma je obavezno polje")]
    [MinLength(1, ErrorMessage = "Minimalna duzina naziva filma je 1 karaktera")]
    [MaxLength(30, ErrorMessage = "Maksimalna duzina naziva filma je 30 karaktera")]
    public required string Naziv { get; set; }

    [Required(ErrorMessage = "Naziv kategorije filma je obavezno polje")]
    [MinLength(1, ErrorMessage = "Minimalna duzina naziva kategorije filma je 1 karaktera")]
    [MaxLength(30, ErrorMessage = "Maksimalna duzina naziva kategorije filma je 30 karaktera")]
    public required string Kategorija { get; set; }

    [Required(ErrorMessage = "Prosecna ocena filma je obavezno polje")]
    [DefaultValue(0.0)]
    [Range(0.0, 10.0, ErrorMessage = "Prosecna ocena filma moze da bude u opsegu od 0.0 do 10.0")]
    public required double ProsecnaOcena { get; set; } = 0.0;

    [Required(ErrorMessage = "Broj ocena filma je obavezno polje")]
    [DefaultValue(0)]
    public required int BrojOcena { get; set; } = 0;

    [DefaultValue(null)]
    [JsonIgnore]
    public ProducentskaKuca? ProducentskaKuca { get; set; } = null;
}