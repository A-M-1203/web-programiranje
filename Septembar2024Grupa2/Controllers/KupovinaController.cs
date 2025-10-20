using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Septembar2024Grupa2.Models;
using Septembar2024Grupa2.Requests;

namespace Septembar2024Grupa2.Controllers;

[ApiController]
[Route("api")]
public class KupovinaController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public KupovinaController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("kupovine")]
    public async Task<ActionResult> NapraviKupovinu([FromBody] CreateKupovinaRequest request)
    {
        var kupovina = new Kupovina
        {
            UkupnaCena = 0
        };

        _dbContext.Kupovine.Add(kupovina);

        foreach (var proizvodId in request.ProizvodiIds)
        {
            var proizvod = await _dbContext.Proizvodi.FirstOrDefaultAsync(x => x.Id == proizvodId);
            if (proizvod is not null && proizvod.Kolicina > 0)
            {
                kupovina.UkupnaCena += proizvod.Cena;
                proizvod.Kolicina -= 1;
                _dbContext.ProizvodiKupovine.Add(new ProizvodKupovina
                {
                    Proizvod = proizvod,
                    Kupovina = kupovina
                });
            }
        }

        await _dbContext.SaveChangesAsync();

        return Created();
    }
}