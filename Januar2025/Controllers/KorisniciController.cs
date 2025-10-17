using Januar2025.Models;
using Januar2025.Requests.KorisnikRequests;
using Januar2025.Responses.AutomobilResponses;
using Januar2025.Responses.KorisnikResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Januar2025.Controllers;

[ApiController]
public class KorisniciController : ControllerBase
{
    private readonly AppDbContext _context;

    public KorisniciController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("korisnici/jmbg/{Jmbg}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetByJmbg(string Jmbg)
    {
        var korisnik = await _context.Korisnici
                                .Include(x => x.Iznajmljivanja)
                                .ThenInclude(x => x.Automobil)
                                .FirstOrDefaultAsync(x => x.Jmbg == Jmbg);
        if (korisnik == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Korisnik ne postoji",
                detail: "Ne postoji korisnik sa zadatim JMBG-om",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        var response = new KorisnikResponse
        {
            Id = korisnik.Id,
            Ime = korisnik.Ime,
            Prezime = korisnik.Prezime,
            Jmbg = korisnik.Jmbg,
            BrojVozacke = korisnik.BrojVozacke,
            Iznajmljivanja = korisnik.Iznajmljivanja.Select(x => new KorisnikIznajmljivanjeResponse
            {
                Id = x.Id,
                Automobil = new AutomobilBasicResponse
                {
                    Id = x.Automobil!.Id,
                    Model = x.Automobil.Model,
                    Kilometraza = x.Automobil.Kilometraza,
                    Godiste = x.Automobil.Godiste,
                    BrojSedista = x.Automobil.BrojSedista,
                    CenaPoDanu = x.Automobil.CenaPoDanu,
                    TrenutnoIznajmljen = x.Automobil.TrenutnoIznajmljen
                },
                BrojDana = x.BrojDana
            }).ToList()
        };

        return Ok(response);
    }

    [HttpGet("korisnici/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

        var korisnik = await _context.Korisnici
                        .Include(x => x.Iznajmljivanja)
                        .ThenInclude(x => x.Automobil)
                        .FirstOrDefaultAsync(x => x.Id == id);

        if (korisnik == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Korisnik ne postoji",
                detail: "Ne postoji korisnik sa zadatim Id-jem",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        var response = new KorisnikResponse
        {
            Id = korisnik.Id,
            Ime = korisnik.Ime,
            Prezime = korisnik.Prezime,
            Jmbg = korisnik.Jmbg,
            BrojVozacke = korisnik.BrojVozacke,
            Iznajmljivanja = korisnik.Iznajmljivanja.Select(x => new KorisnikIznajmljivanjeResponse
            {
                Id = x.Id,
                Automobil = new AutomobilBasicResponse
                {
                    Id = x.Automobil!.Id,
                    Model = x.Automobil.Model,
                    Kilometraza = x.Automobil.Kilometraza,
                    Godiste = x.Automobil.Godiste,
                    BrojSedista = x.Automobil.BrojSedista,
                    CenaPoDanu = x.Automobil.CenaPoDanu,
                    TrenutnoIznajmljen = x.Automobil.TrenutnoIznajmljen
                },
                BrojDana = x.BrojDana
            }).ToList()
        };

        return Ok(response);
    }

    [HttpPost("korisnici")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromBody] CreateKorisnikRequest korisnik)
    {
        Korisnik noviKorisnik = new()
        {
            Ime = korisnik.Ime,
            Prezime = korisnik.Prezime,
            Jmbg = korisnik.Jmbg,
            BrojVozacke = korisnik.BrojVozacke
        };

        _context.Korisnici.Add(noviKorisnik);
        int result = await _context.SaveChangesAsync();

        if (result == 0)
        {
            return Problem
            (
                type: "Server Error",
                title: "Greska pri cuvanju podataka",
                detail: "Korisnik nije sacuvan",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }

        var response = new KorisnikResponse
        {
            Id = noviKorisnik.Id,
            Ime = noviKorisnik.Ime,
            Prezime = noviKorisnik.Prezime,
            Jmbg = noviKorisnik.Jmbg,
            BrojVozacke = noviKorisnik.BrojVozacke
        };

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("korisnici")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Update([FromBody] UpdateKorisnikRequest korisnik)
    {
        if (korisnik.Id < 1)
        {
            return Problem
            (
                type: "Bad Request",
                title: "Nevalidan Id",
                detail: "Id ne moze da bude manji od 1",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var postojeciKorisnik = await _context.Korisnici
                                        .Include(x => x.Iznajmljivanja)
                                        .ThenInclude(x => x.Automobil)
                                        .FirstOrDefaultAsync(x => x.Id == korisnik.Id);
        if (postojeciKorisnik == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Korisnik ne postoji",
                detail: "Ne postoji korisnik sa zadatim Id-jem",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        postojeciKorisnik.Ime = korisnik.Ime;
        postojeciKorisnik.Prezime = korisnik.Prezime;
        postojeciKorisnik.Jmbg = korisnik.Jmbg;
        postojeciKorisnik.BrojVozacke = korisnik.BrojVozacke;

        await _context.SaveChangesAsync();

        var response = new KorisnikResponse
        {
            Id = postojeciKorisnik.Id,
            Ime = postojeciKorisnik.Ime,
            Prezime = postojeciKorisnik.Prezime,
            Jmbg = postojeciKorisnik.Jmbg,
            BrojVozacke = postojeciKorisnik.BrojVozacke,
            Iznajmljivanja = postojeciKorisnik.Iznajmljivanja.Select(x => new KorisnikIznajmljivanjeResponse
            {
                Id = x.Id,
                Automobil = new AutomobilBasicResponse
                {
                    Id = x.Automobil!.Id,
                    Model = x.Automobil.Model,
                    Kilometraza = x.Automobil.Kilometraza,
                    Godiste = x.Automobil.Godiste,
                    BrojSedista = x.Automobil.BrojSedista,
                    CenaPoDanu = x.Automobil.CenaPoDanu,
                    TrenutnoIznajmljen = x.Automobil.TrenutnoIznajmljen
                },
                BrojDana = x.BrojDana
            }).ToList()
        };

        return Ok(response);
    }

    [HttpDelete("korisnici/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
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

        var korisnik = await _context.Korisnici
                                .Include(x => x.Iznajmljivanja)
                                .ThenInclude(x => x.Automobil)
                                .FirstOrDefaultAsync(x => x.Id == id);
        if (korisnik == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Korisnik ne postoji",
                detail: "Ne postoji korisnik sa zadatim Id-jem",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        foreach (var iznajmljivanje in korisnik.Iznajmljivanja)
        {
            iznajmljivanje.Automobil!.TrenutnoIznajmljen = false;
            _context.Iznajmljivanja.Remove(iznajmljivanje);
        }

        _context.Korisnici.Remove(korisnik);
        await _context.SaveChangesAsync();

        return Ok();
    }
}