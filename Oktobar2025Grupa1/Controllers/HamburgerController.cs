using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oktobar2025Grupa1.Models;
using Oktobar2025Grupa1.Requests;

namespace Oktobar2025Grupa1.Controllers;

[ApiController]
public class HamburgerController : ControllerBase
{
    private readonly AppDbContext _context;

    public HamburgerController(AppDbContext context)
    {
        _context = context;
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("hamburgeri")]
    public async Task<ActionResult> Create([FromBody] CreateHamburgerRequest request)
    {
        var prodavnica = await _context.Prodavnice
                                .Include(x => x.Hamburgeri)
                                .FirstOrDefaultAsync(x => x.Naziv == request.NazivProdavnice);

        if (prodavnica is null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Prodavnica ne postoji",
                detail: "Prodavnica sa navedenim nazivom ne postoji",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        if (prodavnica.Hamburgeri.Count >= prodavnica.Kapacitet)
        {
            return Problem
            (
                type: "Bad Request",
                title: "Prodavnica je puna",
                detail: "Prodavnica sa navedenim nazivom ne moze da sprema vise hamburgera",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var hamburger = new Hamburger
        {
            Naziv = "Ham" + prodavnica.Hamburgeri.Count,
            Prodat = false,
            Prodavnica = prodavnica
        };

        _context.Hamburgeri.Add(hamburger);
        await _context.SaveChangesAsync();

        return Created();
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPut("hamburgeri/sastojci")]
    public async Task<ActionResult> UpdateSastojci([FromBody] UpdateSastojciHamburgeraRequest request)
    {
        var hamburger = await _context.Hamburgeri
                                    .Include(x => x.Prodavnica)
                                    .Include(x => x.Sastojci)
                                    .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (hamburger is null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Hamburger ne postoji",
                detail: "Hamburger sa navedenim Id-jem ne postoji",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        foreach (var sastojak in request.Sastojci)
        {
            var postojeciSastojak = await _context.Sastojci
                                        .Include(x => x.Hamburgeri)
                                        .FirstOrDefaultAsync(x => x.Naziv == sastojak);

            if (postojeciSastojak is null)
            {
                return Problem
                (
                    type: "Not Found",
                    title: "Sastojak ne postoji",
                    detail: "Sastojak sa navedenim nazivom ne postoji",
                    statusCode: StatusCodes.Status404NotFound
                );
            }

            hamburger.Sastojci.Add(postojeciSastojak);
            postojeciSastojak.Hamburgeri.Add(hamburger);
        }

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Id = hamburger.Id,
            Naziv = hamburger.Naziv,
            Prodat = hamburger.Prodat,
            Prodavnica = hamburger.Prodavnica == null ? null : new
            {
                Naziv = hamburger.Prodavnica.Naziv,
                Kapacitet = hamburger.Prodavnica.Kapacitet
            },
            Sastojci = (hamburger.Sastojci == null || hamburger.Sastojci.Count == 0) ? null
            : hamburger.Sastojci.Select(x => new
            {
                Naziv = x.Naziv,
                Debljina = x.Debljina
            })
        });
    }

    [HttpPut("hamburgeri/prodaj")]
    public async Task<ActionResult> UpdateProdat([FromBody] UpdateProdatHamburgerRequest request)
    {
        var hamburger = await _context.Hamburgeri
                                .Include(x => x.Prodavnica)
                                .Include(x => x.Sastojci)
                                .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (hamburger is null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Hamburger ne postoji",
                detail: "Hamburger sa navedenim Id-jem ne postoji",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        hamburger.Prodat = request.Prodat;
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Id = hamburger.Id,
            Naziv = hamburger.Naziv,
            Prodat = hamburger.Prodat,
            Prodavnica = hamburger.Prodavnica == null ? null : new
            {
                Naziv = hamburger.Prodavnica.Naziv,
                Kapacitet = hamburger.Prodavnica.Kapacitet
            },
            Sastojci = (hamburger.Sastojci == null || hamburger.Sastojci.Count == 0) ? null
            : hamburger.Sastojci.Select(x => new
            {
                Naziv = x.Naziv,
                Debljina = x.Debljina
            })
        });
    }
}