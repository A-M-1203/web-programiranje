using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oktobar2025Grupa2.Models;
using Oktobar2025Grupa2.Requests;
using Oktobar2025Grupa2.Responses;

namespace Oktobar2025Grupa2.Controllers;

[ApiController]
public class ProducentskaKucaController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProducentskaKucaController(AppDbContext context)
    {
        _context = context;
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("producentske-kuce/{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        if (id < 1)
        {
            return Problem
            (
                type: "Bad Request",
                title: "Nevalidan Id",
                detail: "Id ne moze da bude manji od 1",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var producentskaKuca = await _context.ProducentskeKuce
                                        .Include(x => x.Filmovi)
                                        .FirstOrDefaultAsync(x => x.Id == id);

        if (producentskaKuca == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Producentska kuca ne postoji",
                detail: "Ne postoji producentska kuca sa navedenim Id-jem",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        var response = new ProducentskaKucaResponse
        {
            Id = producentskaKuca.Id,
            Naziv = producentskaKuca.Naziv,
            Filmovi = producentskaKuca.Filmovi
        };

        return Ok(response);
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("producentske-kuce-naziv/{naziv}")]
    public async Task<ActionResult> GetByName(string naziv)
    {
        if (string.IsNullOrEmpty(naziv))
        {
            return Problem
            (
                type: "Bad Request",
                title: "Nevalidan naziv",
                detail: "Naziv producentske kuce ne moze da bude prazan string",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var producentskaKuca = await _context.ProducentskeKuce
                                        .Include(x => x.Filmovi)
                                        .FirstOrDefaultAsync(x => x.Naziv == naziv);
        if (producentskaKuca == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Producentska kuca ne postoji",
                detail: "Ne postoji producentska kuca sa navedenim nazivom",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        var response = new ProducentskaKucaResponse
        {
            Id = producentskaKuca.Id,
            Naziv = producentskaKuca.Naziv,
            Filmovi = producentskaKuca.Filmovi
        };

        return Ok(response);
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("producentske-kuce")]
    public async Task<ActionResult> Create([FromBody] CreateProducentskaKucaRequest request)
    {
        var producentskaKuca = await _context.ProducentskeKuce
                                            .FirstOrDefaultAsync(x => x.Naziv == request.Naziv);

        if (producentskaKuca != null)
        {
            return Problem
            (
                type: "Bad Request",
                title: "Producentska kuca vec postoji",
                detail: "Vec postoji producentska kuca sa navedenim nazivom",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        producentskaKuca = new ProducentskaKuca
        {
            Naziv = request.Naziv
        };

        _context.ProducentskeKuce.Add(producentskaKuca);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { Id = producentskaKuca.Id }, producentskaKuca);
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("producentske-kuce/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (id < 1)
        {
            return Problem
            (
                type: "Bad Request",
                title: "Nevalidan Id",
                detail: "Id ne moze da bude manji od 1",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var producentskaKuca = await _context.ProducentskeKuce.FirstOrDefaultAsync(x => x.Id == id);
        if (producentskaKuca == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Producentska kuca ne postoji",
                detail: "Ne postoji producentska kuca sa navedenim Id-jem",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        _context.ProducentskeKuce.Remove(producentskaKuca);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("producentske-kuce/kategorije-i-filmovi")]
    public ActionResult GetAll()
    {
        var producentskeKuce = _context.ProducentskeKuce.Include(x => x.Filmovi);
        var response = new List<ProducentskaKucaBasicResponse>();
        foreach (var p in producentskeKuce)
        {
            response.Add(new ProducentskaKucaBasicResponse
            {
                Naziv = p.Naziv,
                Filmovi = p.Filmovi.Select(x => new FilmBasicResponse
                {
                    Naziv = x.Naziv,
                    Kategorija = x.Kategorija
                }).ToList()
            });
        }

        return Ok(response);
    }

    [HttpGet("producentske-kuce/{naziv}/filmovi-i-ocene")]
    public async Task<ActionResult> GetFilmoviOcene(string naziv)
    {
        var producentskaKuca = await _context.ProducentskeKuce
                                        .Include(x => x.Filmovi)
                                        .FirstOrDefaultAsync(x => x.Naziv == naziv);

        if (producentskaKuca == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Producentska kuca ne postoji",
                detail: "Ne postoji producentska kuca sa navedenim nazivom",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        var najgoreOcenjeniFilm = producentskaKuca.Filmovi
                                    .Select(x => new { x.Naziv, x.ProsecnaOcena })
                                    .OrderBy(x => x.ProsecnaOcena)
                                    .FirstOrDefault();


        var najboljeOcenjeniFilm = producentskaKuca.Filmovi
                                    .Select(x => new { x.Naziv, x.ProsecnaOcena })
                                    .OrderByDescending(x => x.ProsecnaOcena)
                                    .FirstOrDefault();

        var ukupnoFilmova = producentskaKuca.Filmovi.Count;
        var srednjiIndex = ukupnoFilmova / 2;
        var srednjeOcenjeniFilm = producentskaKuca.Filmovi
                                    .Select(x => new { x.Naziv, x.ProsecnaOcena })
                                    .OrderBy(x => x.ProsecnaOcena)
                                    .Skip(srednjiIndex)
                                    .Take(1)
                                    .FirstOrDefault();

        return Ok(new
        {
            Najgore = najgoreOcenjeniFilm,
            Srednje = srednjeOcenjeniFilm,
            Najbolje = najboljeOcenjeniFilm
        });
    }
}