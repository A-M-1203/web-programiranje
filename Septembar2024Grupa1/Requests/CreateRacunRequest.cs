using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Septembar2024Grupa1.Requests;

public class CreateRacunRequest
{
    [Required(ErrorMessage = "Id stana je obavezno polje")]
    public required int StanId { get; set; }

    [Required(ErrorMessage = "Mesec izdavanja racuna je obavezno polje")]
    [Range(1, 12, ErrorMessage = "Mesec izdavanja racuna je vrednost od 1 do 12")]
    public required int MesecIzdavanja { get; set; }

    [Required(ErrorMessage = "Cena vode je obavezno polje")]
    public required int CenaVode { get; set; }

    [Required(ErrorMessage = "Da li je racun placen je obavezno polje")]
    [DefaultValue(false)]
    public required bool Placen { get; set; } = false;
}