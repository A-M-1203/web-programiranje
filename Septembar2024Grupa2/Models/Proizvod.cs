using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Septembar2024Grupa2.Models;

public class Proizvod
{
    public int Id { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(50)]
    public required string Naziv { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    [DefaultValue(0)]
    public required int Kolicina { get; set; } = 0;

    [Required]
    public required int Cena { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(100)]
    public required string KratakOpis { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(300)]
    public required string DuziOpis { get; set; }

    [Required]
    [MinLength(1)]
    [MaxLength(30)]
    public required string Kategorija { get; set; }

    [JsonIgnore]
    public List<ProizvodKupovina> Kupovine { get; set; } = [];
}