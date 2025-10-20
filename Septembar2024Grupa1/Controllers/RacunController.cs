using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Septembar2024Grupa1.Models;
using Septembar2024Grupa1.Requests;

namespace Septembar2024Grupa1.Controllers;

[ApiController]
public class RacunController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public RacunController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("racuni")]
    public async Task<ActionResult> Create([FromBody] CreateRacunRequest request)
    {
        var stan = await _dbContext.Stanovi
                            .Include(x => x.Racuni)
                            .FirstOrDefaultAsync(x => x.Id == request.StanId);

        if (stan is null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Stan ne postoji",
                detail: "Ne postoji stan sa navedenim Id-jem",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        var racun = new Racun
        {
            Stan = stan,
            MesecIzdavanja = request.MesecIzdavanja,
            CenaStruje = 150 * stan.BrojClanova,
            CenaVode = request.CenaVode,
            CenaKomunalija = 100 * stan.BrojClanova,
            Placen = request.Placen
        };

        _dbContext.Racuni.Add(racun);
        await _dbContext.SaveChangesAsync();

        return Created();
    }
}