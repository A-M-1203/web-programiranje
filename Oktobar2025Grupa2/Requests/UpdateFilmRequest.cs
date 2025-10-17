using System.ComponentModel.DataAnnotations;

namespace Oktobar2025Grupa2.Requests;

public class UpdateFilmRequest
{
    [Required(ErrorMessage = "Naziv filma je obavezno polje")]
    [MinLength(1, ErrorMessage = "Minimalna duzina naziva filma je 1 karaktera")]
    [MaxLength(30, ErrorMessage = "Maksimalna duzina naziva filma je 30 karaktera")]
    public required string Naziv { get; set; }

    [Required(ErrorMessage = "Ocena filma je obavezno polje")]
    [Range(0.0, 10.0, ErrorMessage = "Ocena filma moze da bude u opsegu od 0.0 do 10.0")]
    public required double Ocena { get; set; }
}