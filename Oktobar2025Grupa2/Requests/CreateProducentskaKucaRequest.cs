using System.ComponentModel.DataAnnotations;

namespace Oktobar2025Grupa2.Requests;

public class CreateProducentskaKucaRequest
{
    [Required(ErrorMessage = "Naziv producentske kuce je obavezno polje")]
    [MinLength(1, ErrorMessage = "Minimalna duzina naziva producentske kuce je 1 karaktera")]
    [MaxLength(30, ErrorMessage = "Maksimalna duzina naziva producentske kuce je 30 karaktera")]
    public required string Naziv { get; set; }
}