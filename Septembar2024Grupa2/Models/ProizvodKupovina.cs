using System.ComponentModel.DataAnnotations;

namespace Septembar2024Grupa2.Models;

public class ProizvodKupovina
{
    public int Id { get; set; }

    [Required]
    public required Proizvod Proizvod { get; set; }

    [Required]
    public required Kupovina Kupovina { get; set; }
}