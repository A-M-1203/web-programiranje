using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Septembar2024Grupa1.Models;
using Septembar2024Grupa1.Requests;

namespace Septembar2024Grupa1.Controllers;

[ApiController]
[Route("api")]
public class StanController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public StanController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("stanovi/ids")]
    public async Task<ActionResult> GetAllIds()
    {
        return Ok(await _dbContext.Stanovi.Select(x => x.Id).ToListAsync());
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("stanovi/{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var stan = await _dbContext.Stanovi
                                .Include(x => x.Racuni)
                                .FirstOrDefaultAsync(x => x.Id == id);

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

        return Ok(new
        {
            Id = stan.Id,
            ImeVlasnika = stan.ImeVlasnika,
            Povrsina = stan.Povrsina,
            BrojClanova = stan.BrojClanova,
            Racuni = stan.Racuni.Select(x => new
            {
                Id = x.Id,
                MesecIzdavanja = x.MesecIzdavanja,
                CenaStruje = x.CenaStruje,
                CenaVode = x.CenaVode,
                CenaKomunalija = x.CenaKomunalija,
                Placen = x.Placen ? "Da" : "Ne"
            }),
        });
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("stanovi")]
    public async Task<ActionResult> Create([FromBody] CreateStanRequest request)
    {
        var stan = new Stan
        {
            ImeVlasnika = request.ImeVlasnika,
            Povrsina = request.Povrsina,
            BrojClanova = request.BrojClanova
        };

        _dbContext.Stanovi.Add(stan);
        await _dbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { Id = stan.Id }, stan);
    }

    [HttpGet("stanovi/{id}/troskovi")]
    public async Task<ActionResult> VratiUkupneNeizmireneTroskove(int id)
    {
        var stan = await _dbContext.Stanovi
                                .Include(x => x.Racuni)
                                .FirstOrDefaultAsync(x => x.Id == id);

        if (stan is null)
        {
            return NotFound("Ne postoji stan sa navedenim Id-jem");
        }

        int ukupniNeizmireniTroskovi = 0;
        foreach (var racun in stan.Racuni)
        {
            if (racun.Placen == false)
            {
                ukupniNeizmireniTroskovi += racun.CenaStruje;
                ukupniNeizmireniTroskovi += racun.CenaVode;
                ukupniNeizmireniTroskovi += racun.CenaKomunalija;
            }
        }

        return Ok(ukupniNeizmireniTroskovi);
    }
}