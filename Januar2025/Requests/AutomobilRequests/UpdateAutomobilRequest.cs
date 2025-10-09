using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Januar2025.Requests.AutomobilRequests;

public class UpdateAutomobilRequest
{
    [Required(ErrorMessage = "Id je obavezno polje.")]
    public required int Id { get; set; }

    [MinLength(2, ErrorMessage = "Minimalna duzina modela automobila je 2 karaktera.")]
    [MaxLength(30, ErrorMessage = "Maksimalna duzina modela automobila je 2 karaktera.")]
    [Required(ErrorMessage = "Model je obavezno polje.")]
    public required string Model { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Minimalna kilometraza je 0.")]
    [Required(ErrorMessage = "Kilometraza je obavezno polje.")]
    public required int Kilometraza { get; set; }

    [Range(1900, int.MaxValue, ErrorMessage = "Minimalno godiste je 1900.")]
    [Required(ErrorMessage = "Godiste je obavezno polje.")]
    public required int Godiste { get; set; }

    [Range(2, 10, ErrorMessage = "Minimalan broj sedista je 2, a maksimalan 10.")]
    [Required(ErrorMessage = "Broj sedista je obavezno polje.")]
    public required int BrojSedista { get; set; }

    [Range(10, int.MaxValue, ErrorMessage = "Minimalna cena po danu je 10.")]
    [Required(ErrorMessage = "Cena po danu je obavezno polje.")]
    public required int CenaPoDanu { get; set; }

    [Required(ErrorMessage = "Trenutno iznajmljen je obavezno polje.")]
    [DefaultValue(false)]
    public required bool TrenutnoIznajmljen { get; set; } = false;
}