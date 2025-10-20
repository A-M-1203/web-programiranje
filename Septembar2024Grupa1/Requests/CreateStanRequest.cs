using System.ComponentModel.DataAnnotations;

namespace Septembar2024Grupa1.Requests;

public class CreateStanRequest
{
    [Required(ErrorMessage = "Ime vlasnika je obavezno polje")]
    [MinLength(2, ErrorMessage = "Ime vlasnika mora da ima barem 2 karaktera")]
    [MaxLength(50, ErrorMessage = "Ime vlasnika moze da ima najvise 50 karaktera")]
    public required string ImeVlasnika { get; set; }

    [Required(ErrorMessage = "Povrsina stana je obavezno polje")]
    [Range(10, 200, ErrorMessage = "Povrsina stana je vrednost izmedju 10 i 200")]
    public required int Povrsina { get; set; }

    [Required(ErrorMessage = "Broj clanova u stanu je obavezno polje")]
    [Range(1, 20, ErrorMessage = "Broj clanova u stanu je vrednost izmedju 1 i 20")]
    public required int BrojClanova { get; set; }
}