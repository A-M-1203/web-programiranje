using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Septembar2024Grupa2.Models;

public class Kupovina
{
    public int Id { get; set; }

    [Required]
    public required int UkupnaCena { get; set; }

    [JsonIgnore]
    public List<ProizvodKupovina> Proizvodi { get; set; } = [];
}