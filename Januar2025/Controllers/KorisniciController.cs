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

    [HttpGet("korisnici/{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        if (id < 0)
        {
            return BadRequest("Nevalidan Id");
        }

        var korisnik = await _context.Korisnici
                        .Include(x => x.Iznajmljivanja)
                        .ThenInclude(x => x.Automobil)
                        .FirstOrDefaultAsync(x => x.Id == id);

        if (korisnik == null)
        {
            return NotFound("Korisnik ne postoji.");
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
            return BadRequest("Korisnik nije sacuvan.");
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
    public async Task<ActionResult> Update([FromBody] UpdateKorisnikRequest korisnik)
    {
        if (korisnik.Id < 0)
        {
            return BadRequest("Nevalidan Id.");
        }

        var postojeciKorisnik = await _context.Korisnici
                                        .Include(x => x.Iznajmljivanja)
                                        .ThenInclude(x => x.Automobil)
                                        .FirstOrDefaultAsync(x => x.Id == korisnik.Id);
        if (postojeciKorisnik == null)
        {
            return NotFound("Korisnik ne postoji.");
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
    public async Task<ActionResult> Delete(int id)
    {
        if (id < 0)
        {
            return BadRequest("Nevalidan Id.");
        }

        var korisnik = await _context.Korisnici
                                .Include(x => x.Iznajmljivanja)
                                .ThenInclude(x => x.Automobil)
                                .FirstOrDefaultAsync(x => x.Id == id);
        if (korisnik == null)
        {
            return NotFound("Korisnik ne postoji.");
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