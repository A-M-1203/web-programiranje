using System.ComponentModel.DataAnnotations;

namespace Septembar2024Grupa2.Requests;

public class CreateKupovinaRequest
{
    [Required(ErrorMessage = "Kupljeni proizvodi je obavezno polje")]
    public List<int> ProizvodiIds { get; set; } = [];
}