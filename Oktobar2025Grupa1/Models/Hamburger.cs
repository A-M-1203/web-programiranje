using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oktobar2025Grupa1.Models;

public class Hamburger
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Naziv hamburgera je obavezno polje")]
    [MinLength(1, ErrorMessage = "Naziv hamburgera mora da ima barem 1 karakter")]
    [MaxLength(30, ErrorMessage = "Naziv hamburgera moze da ima najvise 30 karaktera")]
    public required string Naziv { get; set; }

    [DefaultValue(false)]
    public bool Prodat { get; set; } = false;
    public Prodavnica? Prodavnica { get; set; }

    [JsonIgnore]
    public List<Sastojak> Sastojci { get; set; } = [];
}