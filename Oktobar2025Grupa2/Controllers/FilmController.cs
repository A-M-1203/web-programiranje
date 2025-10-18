using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oktobar2025Grupa2.Models;
using Oktobar2025Grupa2.Requests;

namespace Oktobar2025Grupa2.Controllers;

[ApiController]
public class FilmController : ControllerBase
{
    private readonly AppDbContext _context;

    public FilmController(AppDbContext context)
    {
        _context = context;
    }


    [HttpGet("filmovi/kategorija/{kategorija}")]
    public ActionResult GetFilmByKategorija(string kategorija)
    {
        // var filmovi = _context.Filmovi.Where(x=>x.Kategorija == kategorija).Select(x=>x.Naziv);
        return Ok(_context.Filmovi.Where(x => x.Kategorija == kategorija).Select(x => x.Naziv));
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("filmovi/{id}")]
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

        var film = await _context.Filmovi
                            .Include(x => x.ProducentskaKuca)
                            .FirstOrDefaultAsync(x => x.Id == id);
        if (film == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Film ne postoji",
                detail: "Ne postoji film sa navedenim Id-jem",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        return Ok(new
        {
            Id = film.Id,
            Naziv = film.Naziv,
            Kategorija = film.Kategorija,
            ProsecnaOcena = film.ProsecnaOcena,
            BrojOcena = film.BrojOcena,
            ProducentskaKuca = film.ProducentskaKuca == null ? null : new
            {
                Id = film.ProducentskaKuca.Id,
                Naziv = film.ProducentskaKuca.Naziv
            }
        });
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("filmovi-naziv/{naziv}")]
    public async Task<ActionResult> GetByName(string naziv)
    {
        if (string.IsNullOrEmpty(naziv))
        {
            return Problem
            (
                type: "Bad Request",
                title: "Nevalidan naziv",
                detail: "Naziv filma ne moze da bude prazan string",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var film = await _context.Filmovi
                            .Include(x => x.ProducentskaKuca)
                            .FirstOrDefaultAsync(x => x.Naziv == naziv);
        if (film == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Film ne postoji",
                detail: "Ne postoji film sa navedenim nazivom",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        return Ok(new
        {
            Id = film.Id,
            Naziv = film.Naziv,
            Kategorija = film.Kategorija,
            ProsecnaOcena = film.ProsecnaOcena,
            BrojOcena = film.BrojOcena,
            ProducentskaKuca = film.ProducentskaKuca == null ? null : new
            {
                Id = film.ProducentskaKuca.Id,
                Naziv = film.ProducentskaKuca.Naziv
            }
        });
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("filmovi")]
    public async Task<ActionResult> Create([FromBody] CreateFilmRequest request)
    {
        var film = await _context.Filmovi.FirstOrDefaultAsync(x => x.Naziv == request.Naziv);

        if (film != null)
        {
            return Problem
            (
                type: "Bad Request",
                title: "Film vec postoji",
                detail: "Vec postoji film sa navedenim nazivom",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var producentskaKuca = await _context.ProducentskeKuce
                                        .FirstOrDefaultAsync(x => x.Id == request.ProducentskaKucaId);
        if (producentskaKuca == null)
        {
            return Problem
            (
                type: "Bad Request",
                title: "Producentska kuca ne postoji",
                detail: "Ne postoji producentska kuca sa navedenim Id-jem",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        film = new Film
        {
            Naziv = request.Naziv,
            Kategorija = request.Kategorija,
            ProsecnaOcena = 0.0,
            BrojOcena = 0,
            ProducentskaKuca = producentskaKuca
        };

        _context.Filmovi.Add(film);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { Id = film.Id }, new
        {
            Id = film.Id,
            Naziv = film.Naziv,
            Kategorija = film.Kategorija,
            ProsecnaOcena = film.ProsecnaOcena,
            BrojOcena = film.BrojOcena,
            ProducentskaKuca = new
            {
                Id = producentskaKuca.Id,
                Naziv = producentskaKuca.Naziv
            }
        });
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPut("filmovi")]
    public async Task<ActionResult> Update([FromBody] UpdateFilmRequest request)
    {
        var film = await _context.Filmovi
                                .Include(x => x.ProducentskaKuca)
                                .FirstOrDefaultAsync(x => x.Naziv == request.Naziv);

        if (film == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Film ne postoji",
                detail: "Ne postoji film sa navedenim nazivom",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        film.ProsecnaOcena = ((film.ProsecnaOcena * film.BrojOcena) + request.Ocena) / (film.BrojOcena + 1);
        film.BrojOcena++;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            Id = film.Id,
            Naziv = film.Naziv,
            Kategorija = film.Kategorija,
            ProsecnaOcena = film.ProsecnaOcena,
            BrojOcena = film.BrojOcena,
            ProducentskaKuca = film.ProducentskaKuca == null ? null : new
            {
                Id = film.ProducentskaKuca.Id,
                Naziv = film.ProducentskaKuca.Naziv
            }
        });
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpDelete("filmovi/{id}")]
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

        var film = await _context.Filmovi.FirstOrDefaultAsync(x => x.Id == id);
        if (film == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Film ne postoji",
                detail: "Ne postoji film sa navedenim Id-jem",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        _context.Filmovi.Remove(film);
        await _context.SaveChangesAsync();

        return Ok();
    }
}