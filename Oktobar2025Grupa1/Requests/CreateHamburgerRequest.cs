using System.ComponentModel.DataAnnotations;

namespace Oktobar2025Grupa1.Requests;

public class CreateHamburgerRequest
{
    [Required(ErrorMessage = "Naziv prodavnice je obavezno polje")]
    [MinLength(1, ErrorMessage = "Naziv prodavnice mora da ima barem 1 karakter")]
    [MaxLength(30, ErrorMessage = "Naziv prodavnice moze da ima najvise 30 karaktera")]
    public required string NazivProdavnice { get; set; }
}