using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Septembar2024Grupa1.Models;

public class Stan
{
    public int Id { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(50)]
    public required string ImeVlasnika { get; set; }

    [Required]
    [Range(10, 200)]
    public required int Povrsina { get; set; }

    [Required]
    [Range(1, 20)]
    public required int BrojClanova { get; set; }

    [JsonIgnore]
    public List<Racun> Racuni { get; set; } = [];
}