using System.ComponentModel.DataAnnotations;

namespace Januar2025.Requests.IznajmljivanjeRequests;

public class UpdateIznajmljivanjeRequest
{
    [Required(ErrorMessage = "Id je obavezno polje")]
    public required int Id { get; set; }

    [Required(ErrorMessage = "AutomobilId je obavezno polje.")]
    public required int AutomobilId { get; set; }

    [Required(ErrorMessage = "KorisnikId je obavezno polje.")]
    public required int KorisnikId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Minimalna vrednost za broj dana je 1.")]
    [Required(ErrorMessage = "Broj dana je obavezno polje.")]
    public required int BrojDana { get; set; }
}