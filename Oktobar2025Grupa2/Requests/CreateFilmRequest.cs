using System.ComponentModel.DataAnnotations;

namespace Oktobar2025Grupa2.Requests;

public class CreateFilmRequest
{
    [Required(ErrorMessage = "Naziv filma je obavezno polje")]
    [MinLength(1, ErrorMessage = "Minimalna duzina naziva filma je 1 karaktera")]
    [MaxLength(30, ErrorMessage = "Maksimalna duzina naziva filma je 30 karaktera")]
    public required string Naziv { get; set; }

    [Required(ErrorMessage = "Naziv kategorije filma je obavezno polje")]
    [MinLength(1, ErrorMessage = "Minimalna duzina naziva kategorije filma je 1 karaktera")]
    [MaxLength(30, ErrorMessage = "Maksimalna duzina naziva kategorije filma je 30 karaktera")]
    public required string Kategorija { get; set; }

    [Required(ErrorMessage = "Id producentske kuce je obavezno polje")]
    public required int ProducentskaKucaId { get; set; }
}