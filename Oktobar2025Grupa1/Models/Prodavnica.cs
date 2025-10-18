using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oktobar2025Grupa1.Models;

public class Prodavnica
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Naziv prodavnice je obavezno polje")]
    [MinLength(1, ErrorMessage = "Naziv prodavnice mora da ima barem 1 karakter")]
    [MaxLength(30, ErrorMessage = "Naziv prodavnice moze da ima najvise 30 karaktera")]
    public required string Naziv { get; set; }

    [Required(ErrorMessage = "Kapacitet prodavnice je obavezno polje")]
    [Range(1, 10, ErrorMessage = "Kapacitet prodavnice moze da bude u opsegu od 1 do 10")]
    [DefaultValue(5)]
    public required int Kapacitet { get; set; }

    [JsonIgnore]
    public List<Hamburger> Hamburgeri { get; set; } = [];
}