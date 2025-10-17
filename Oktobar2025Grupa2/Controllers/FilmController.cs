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

        return Ok(film);
    }

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

        var film = await _context.Filmovi.FirstOrDefaultAsync(x => x.Naziv == naziv);
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

        return Ok(film);
    }

    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("filmovi")]
    public async Task<ActionResult> Create([FromBody] CreateFilmRequest request)
    {
        var film = await _context.Filmovi
                                .Include(x => x.ProducentskaKuca)
                                .FirstOrDefaultAsync(x => x.Naziv == request.Naziv);

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

        return CreatedAtAction(nameof(GetById), new { Id = film.Id }, film);
    }

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

        return Ok(film);
    }

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