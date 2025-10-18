using System.ComponentModel;

namespace Oktobar2025Grupa1.Requests;

public class UpdateProdatHamburgerRequest
{
    public required int Id { get; set; }

    [DefaultValue(true)]
    public required bool Prodat { get; set; } = true;
}