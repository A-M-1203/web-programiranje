using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Oktobar2025Grupa1.Requests;

public class CreateSastojakRequest
{
    [Required(ErrorMessage = "Naziv sastojka je obavezno polje")]
    [MinLength(1, ErrorMessage = "Naziv sastojka mora da ima barem 1 karakter")]
    [MaxLength(30, ErrorMessage = "Naziv sastojka moze da ima najvise 30 karaktera")]
    public required string Naziv { get; set; }

    [Required(ErrorMessage = "Debljina sastojka je obavezno polje")]
    [Range(10, 100, ErrorMessage = "Debljina sastojka moze da bude u opsegu od 10 do 100")]
    [DefaultValue(20)]
    public required int Debljina { get; set; }
}