using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oktobar2025Grupa1.Models;
using Oktobar2025Grupa1.Requests;

namespace Oktobar2025Grupa1.Controllers;

[ApiController]
public class ProdavnicaController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProdavnicaController(AppDbContext context)
    {
        _context = context;
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("prodavnice")]
    public ActionResult GetAll()
    {
        var prodavnice = _context.Prodavnice.Include(x => x.Hamburgeri).ThenInclude(x => x.Sastojci);
        var response = new List<object>();
        foreach (var p in prodavnice)
        {
            response.Add(new
            {
                Naziv = p.Naziv,
                Kapacitet = p.Kapacitet,
                Hamburgeri = (p.Hamburgeri == null || p.Hamburgeri.Count == 0) ? null : p.Hamburgeri
                .Select(x => new
                {
                    Id = x.Id,
                    Naziv = x.Naziv,
                    Prodat = x.Prodat,
                    Sastojci = (x.Sastojci == null || x.Sastojci.Count == 0) ? null : x.Sastojci
                    .Select(y => new
                    {
                        Naziv = y.Naziv,
                        Debljina = y.Debljina
                    })
                }),
            });
        }

        return Ok(response);
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("prodavnice")]
    public async Task<ActionResult> Create([FromBody] CreateProdavnicaRequest request)
    {
        var prodavnica = await _context.Prodavnice.FirstOrDefaultAsync(x => x.Naziv == request.Naziv);
        if (prodavnica is not null)
        {
            return Problem
            (
                type: "Bad Request",
                title: "Prodavnica vec postoji",
                detail: "Prodavnica sa navedenim nazivom vec postoji",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        prodavnica = new Prodavnica
        {
            Naziv = request.Naziv,
            Kapacitet = request.Kapacitet,
        };

        _context.Prodavnice.Add(prodavnica);
        await _context.SaveChangesAsync();

        return Created();
    }
}