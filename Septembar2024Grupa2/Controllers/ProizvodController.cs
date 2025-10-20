using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Septembar2024Grupa2.Models;
using Septembar2024Grupa2.Requests;

namespace Septembar2024Grupa2.Controllers;

[ApiController]
[Route("api")]
public class ProizvodController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ProizvodController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("kategorije")]
    public async Task<ActionResult> VratiSveKategorije()
    {
        return Ok(await _dbContext.Proizvodi
                            .Select(x => x.Kategorija)
                            .Distinct()
                            .ToListAsync());
    }

    [HttpGet("proizvodi/{kategorija}")]
    public async Task<ActionResult> VratiProizvodePoKategoriji(string kategorija)
    {
        return Ok(await _dbContext.Proizvodi
                            .Where(x => x.Kategorija == kategorija)
                            .Select(x => new
                            {
                                Id = x.Id,
                                Naziv = x.Naziv,
                                Kolicina = x.Kolicina,
                                Cena = x.Cena,
                                KratakOpis = x.KratakOpis,
                                DuziOpis = x.DuziOpis
                            }).ToListAsync());
    }

    [HttpPost("proizvodi")]
    public async Task<ActionResult> NapraviProizvod([FromBody] CreateProizvodRequest request)
    {
        var proizvod = new Proizvod
        {
            Naziv = request.Naziv,
            Kolicina = request.Kolicina,
            Cena = request.Cena,
            KratakOpis = request.KratakOpis,
            DuziOpis = request.DuziOpis,
            Kategorija = request.Kategorija,
        };

        _dbContext.Proizvodi.Add(proizvod);
        await _dbContext.SaveChangesAsync();

        return Created();
    }
}