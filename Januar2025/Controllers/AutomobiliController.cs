using Azure.Core;
using Januar2025.Models;
using Januar2025.Requests.AutomobilRequests;
using Januar2025.Responses.AutomobilResponses;
using Januar2025.Responses.KorisnikResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Januar2025.Controllers;

[ApiController]
public class AutomobiliController : ControllerBase
{
    private readonly AppDbContext _context;
    public AutomobiliController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("automobili")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll(
        [FromQuery] int? kilometraza,
        [FromQuery] int? brojSedista,
        [FromQuery] int? cena,
        [FromQuery] string? model)
    {
        var query = _context.Automobili.AsQueryable();

        if (kilometraza != null)
        {
            query = query.Where(x => x.Kilometraza == kilometraza);
        }

        if (brojSedista != null)
        {
            query = query.Where(x => x.BrojSedista == brojSedista);
        }

        if (cena != null)
        {
            query = query.Where(x => x.CenaPoDanu == cena);
        }

        if (model != null)
        {
            query = query.Where(x => x.Model == model);
        }

        var automobili = await query.ToListAsync();

        var response = new List<AutomobilBasicResponse>();

        foreach (var automobil in automobili)
        {
            response.Add(new AutomobilBasicResponse
            {
                Id = automobil.Id,
                Model = automobil.Model,
                Kilometraza = automobil.Kilometraza,
                Godiste = automobil.Godiste,
                BrojSedista = automobil.BrojSedista,
                CenaPoDanu = automobil.CenaPoDanu,
                TrenutnoIznajmljen = automobil.TrenutnoIznajmljen
            });
        }

        return Ok(response);
    }

    [HttpGet("automobili/modeli")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAllModels()
    {
        return Ok(await _context.Automobili.Select(x => x.Model).Distinct().ToListAsync());
    }

    [HttpGet("automobili/{id}")]
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
                detail: "Id ne moze da bude manji od 1.",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var automobil = await _context.Automobili
                        .Include(x => x.Iznajmljivanja)
                        .ThenInclude(x => x.Korisnik)
                        .FirstOrDefaultAsync(x => x.Id == id);

        if (automobil == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Automobil nije pronadjen",
                detail: "Nepostoji automobil sa zadatim Id-jem",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        var response = new AutomobilResponse
        {
            Id = automobil.Id,
            Model = automobil.Model,
            Kilometraza = automobil.Kilometraza,
            Godiste = automobil.Godiste,
            BrojSedista = automobil.BrojSedista,
            CenaPoDanu = automobil.CenaPoDanu,
            TrenutnoIznajmljen = automobil.TrenutnoIznajmljen,
            Iznajmljivanja = automobil.Iznajmljivanja.Select(x => new AutomobilIznajmljivanjeResponse
            {
                Id = x.Id,
                Korisnik = new KorisnikBasicResponse
                {
                    Id = x.Korisnik!.Id,
                    Ime = x.Korisnik.Ime,
                    Prezime = x.Korisnik.Prezime,
                    Jmbg = x.Korisnik.Jmbg,
                    BrojVozacke = x.Korisnik.BrojVozacke
                },
                BrojDana = x.BrojDana
            }).ToList()
        };

        return Ok(response);
    }

    [HttpPost("automobili")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult> Create([FromBody] CreateAutomobilRequest automobil)
    {
        Automobil noviAutomobil = new()
        {
            Model = automobil.Model,
            Kilometraza = automobil.Kilometraza,
            Godiste = automobil.Godiste,
            BrojSedista = automobil.BrojSedista,
            CenaPoDanu = automobil.CenaPoDanu,
            TrenutnoIznajmljen = automobil.TrenutnoIznajmljen
        };

        _context.Automobili.Add(noviAutomobil);
        int result = await _context.SaveChangesAsync();
        if (result == 0)
        {
            return Problem
            (
                type: "Server Error",
                title: "Greska pri cuvanju podataka",
                detail: "Automobil nije sacuvan",
                statusCode: StatusCodes.Status500InternalServerError
            );
        }

        var response = new AutomobilResponse
        {
            Id = noviAutomobil.Id,
            Model = noviAutomobil.Model,
            Kilometraza = noviAutomobil.Kilometraza,
            Godiste = noviAutomobil.Godiste,
            BrojSedista = noviAutomobil.BrojSedista,
            CenaPoDanu = noviAutomobil.CenaPoDanu,
            TrenutnoIznajmljen = noviAutomobil.TrenutnoIznajmljen
        };

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("automobili")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Update([FromBody] UpdateAutomobilRequest automobil)
    {
        if (automobil.Id < 1)
        {
            return Problem
            (
                type: "Bad Request",
                title: "Nevalidan Id",
                detail: "Id ne moze da bude manji od 1",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        var postojeciAutomobil = await _context.Automobili
                                        .Include(x => x.Iznajmljivanja)
                                        .ThenInclude(x => x.Korisnik)
                                        .FirstOrDefaultAsync(x => x.Id == automobil.Id);
        if (postojeciAutomobil == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Automobil nije pronadjen",
                detail: "Ne postoji automobil sa zadatim Id-jem",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        postojeciAutomobil.Model = automobil.Model;
        postojeciAutomobil.Kilometraza = automobil.Kilometraza;
        postojeciAutomobil.Godiste = automobil.Godiste;
        postojeciAutomobil.BrojSedista = automobil.BrojSedista;
        postojeciAutomobil.CenaPoDanu = automobil.CenaPoDanu;
        postojeciAutomobil.TrenutnoIznajmljen = automobil.TrenutnoIznajmljen;

        await _context.SaveChangesAsync();

        var response = new AutomobilResponse
        {
            Id = postojeciAutomobil.Id,
            Model = postojeciAutomobil.Model,
            Kilometraza = postojeciAutomobil.Kilometraza,
            Godiste = postojeciAutomobil.Godiste,
            BrojSedista = postojeciAutomobil.BrojSedista,
            CenaPoDanu = postojeciAutomobil.CenaPoDanu,
            TrenutnoIznajmljen = postojeciAutomobil.TrenutnoIznajmljen,
            Iznajmljivanja = postojeciAutomobil.Iznajmljivanja.Select(x => new AutomobilIznajmljivanjeResponse
            {
                Id = x.Id,
                Korisnik = new KorisnikBasicResponse
                {
                    Id = x.Korisnik!.Id,
                    Ime = x.Korisnik.Ime,
                    Prezime = x.Korisnik.Prezime,
                    Jmbg = x.Korisnik.Jmbg,
                    BrojVozacke = x.Korisnik.BrojVozacke
                },
                BrojDana = x.BrojDana
            }).ToList()
        };

        return Ok(response);
    }

    [HttpDelete("automobili/{id}")]
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

        var automobil = await _context.Automobili
                                .Include(x => x.Iznajmljivanja)
                                .FirstOrDefaultAsync(x => x.Id == id);

        if (automobil == null)
        {
            return Problem
            (
                type: "Not Found",
                title: "Automobil nije pronadjen",
                detail: "Ne postoji automobil sa zadatim Id-jem",
                statusCode: StatusCodes.Status404NotFound
            );
        }

        foreach (var iznajmljivanje in automobil.Iznajmljivanja)
        {
            iznajmljivanje.Automobil!.TrenutnoIznajmljen = false;
            _context.Iznajmljivanja.Remove(iznajmljivanje);
        }

        _context.Automobili.Remove(automobil);
        await _context.SaveChangesAsync();

        return Ok();
    }
}