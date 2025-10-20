using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Septembar2024Grupa2.Requests;

public class CreateProizvodRequest
{
    [Required(ErrorMessage = "Naziv proizvoda je obavezno polje")]
    [MinLength(1, ErrorMessage = "Minimalna duzina naziva proizvoda je 1 karakter")]
    [MaxLength(50, ErrorMessage = "Maksimalna duzina naziva proizvoda je 50 karaktera")]
    public required string Naziv { get; set; }

    [Required(ErrorMessage = "Kolicina proizvoda je obavezno polje")]
    [Range(0, int.MaxValue, ErrorMessage = "Kolicina proizvoda je broj u opsegu od 0 do 2.147.483.647")]
    [DefaultValue(0)]
    public required int Kolicina { get; set; } = 0;

    [Required(ErrorMessage = "Cena proizvoda je obavezno polje")]
    public required int Cena { get; set; }

    [Required(ErrorMessage = "Kratak opis proizvoda je obavezno polje")]
    [MinLength(1, ErrorMessage = "Minimalna duzina kratkog opisa proizvoda je 1 karakter")]
    [MaxLength(100, ErrorMessage = "Maksimalna duzina kratkog opisa proizvoda je 100 karaktera")]
    public required string KratakOpis { get; set; }

    [Required(ErrorMessage = "Duzi opis proizvoda je obavezno polje")]
    [MinLength(1, ErrorMessage = "Minimalna duzina duzeg opisa proizvoda je 1 karakter")]
    [MaxLength(300, ErrorMessage = "Maksimalna duzina duzeg opisa proizvoda je 300 karakter")]
    public required string DuziOpis { get; set; }

    [Required(ErrorMessage = "Kategorija proizvoda je obavezno polje")]
    [MinLength(1, ErrorMessage = "Minimalna duzina kategorije proizvoda je 1 karakter")]
    [MaxLength(30, ErrorMessage = "Maksimalna duzina kategorije proizvoda je 30 karakter")]
    public required string Kategorija { get; set; }
}