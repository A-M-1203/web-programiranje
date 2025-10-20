using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Septembar2024Grupa1.Models;

public class Racun
{
    public int Id { get; set; }

    [Required]
    public required Stan Stan { get; set; }

    [Required]
    [Range(1, 12)]
    public required int MesecIzdavanja { get; set; }

    [Required]
    public required int CenaStruje { get; set; }

    [Required]
    public required int CenaVode { get; set; }

    [Required]
    public required int CenaKomunalija { get; set; }

    [Required]
    [DefaultValue(false)]
    public required bool Placen { get; set; } = false;
}