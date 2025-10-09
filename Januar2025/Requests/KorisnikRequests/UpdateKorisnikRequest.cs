using System.ComponentModel.DataAnnotations;

namespace Januar2025.Requests.KorisnikRequests;

public class UpdateKorisnikRequest
{
    [Required(ErrorMessage = "Id je obavezno polje.")]
    public required int Id { get; set; }

    [MinLength(2, ErrorMessage = "Minimalna duzina imena je 2 karaktera.")]
    [MaxLength(30, ErrorMessage = "Maksimalna duzina imena je 30 karaktera.")]
    [Required(ErrorMessage = "Ime je obavezno polje.")]
    public required string Ime { get; set; }

    [MinLength(2, ErrorMessage = "Minimalna duzina prezimena je 2 karaktera.")]
    [MaxLength(30, ErrorMessage = "Maksimalna duzina prezimena je 30 karaktera.")]
    [Required(ErrorMessage = "Prezime je obavezno polje.")]
    public required string Prezime { get; set; }

    [StringLength(13, MinimumLength = 13, ErrorMessage = "JMBG mora da ima tacno 13 cifara")]
    [Required(ErrorMessage = "JMBG je obavezno polje")]
    public required string Jmbg { get; set; }

    [StringLength(9, MinimumLength = 9, ErrorMessage = "Broj vozacke dozvole mora da ima tacno 9 cifara")]
    [Required(ErrorMessage = "Broj vozacke dozvole je obavezno polje")]
    public required string BrojVozacke { get; set; }
}