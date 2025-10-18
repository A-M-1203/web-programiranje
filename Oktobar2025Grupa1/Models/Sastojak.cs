using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Oktobar2025Grupa1.Models;

public class Sastojak
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Naziv sastojka je obavezno polje")]
    [MinLength(1, ErrorMessage = "Naziv sastojka mora da ima barem 1 karakter")]
    [MaxLength(30, ErrorMessage = "Naziv sastojka moze da ima najvise 30 karaktera")]
    public required string Naziv { get; set; }

    [Required(ErrorMessage = "Debljina sastojka je obavezno polje")]
    [Range(10, 100, ErrorMessage = "Debljina sastojka moze da bude u opsegu od 10 do 100")]
    [DefaultValue(20)]
    public required int Debljina { get; set; }

    [JsonIgnore]
    public List<Hamburger> Hamburgeri { get; set; } = [];
}