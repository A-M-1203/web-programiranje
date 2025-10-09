using Januar2025.Models;
using Januar2025.Requests.IznajmljivanjeRequests;
using Januar2025.Responses.AutomobilResponses;
using Januar2025.Responses.IznajmljivanjeResponses;
using Januar2025.Responses.KorisnikResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Januar2025.Controllers;

[ApiController]
public class IznajmljivanjaController : ControllerBase
{
    private readonly AppDbContext _context;

    public IznajmljivanjaController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("iznajmljivanja")]
    public async Task<ActionResult> Get(int id)
    {
        if (id < 1)
        {
            return BadRequest("Nevalidan Id.");
        }

        var iznajmljivanje = await _context.Iznajmljivanja
                                .Include(x => x.Automobil)
                                .Include(x => x.Korisnik)
                                .FirstOrDefaultAsync(x => x.Id == id);
        if (iznajmljivanje == null)
        {
            return NotFound("Iznajmljivanje ne postoji.");
        }

        var response = new IznajmljivanjeResponse
        {
            Id = iznajmljivanje.Id,
            Automobil = new AutomobilBasicResponse
            {
                Id = iznajmljivanje.Automobil!.Id,
                Model = iznajmljivanje.Automobil.Model,
                Kilometraza = iznajmljivanje.Automobil.Kilometraza,
                Godiste = iznajmljivanje.Automobil.Godiste,
                BrojSedista = iznajmljivanje.Automobil.BrojSedista,
                CenaPoDanu = iznajmljivanje.Automobil.CenaPoDanu,
                TrenutnoIznajmljen = iznajmljivanje.Automobil.TrenutnoIznajmljen
            },
            Korisnik = new KorisnikBasicResponse
            {
                Id = iznajmljivanje.Korisnik!.Id,
                Ime = iznajmljivanje.Korisnik.Ime,
                Prezime = iznajmljivanje.Korisnik.Prezime,
                Jmbg = iznajmljivanje.Korisnik.Jmbg,
                BrojVozacke = iznajmljivanje.Korisnik.BrojVozacke
            },
            BrojDana = iznajmljivanje.BrojDana
        };

        return Ok(response);
    }

    [HttpPost("iznajmljivanja")]
    public async Task<ActionResult> Create([FromBody] CreateIznajmljivanjeRequest iznajmljivanje)
    {
        if (iznajmljivanje.AutomobilId < 1)
        {
            return BadRequest("Nevalidan AutomobilId.");
        }

        if (iznajmljivanje.KorisnikId < 1)
        {
            return BadRequest("Nevalidan KorisnikId.");
        }

        var automobil = await _context.Automobili
                        .FirstOrDefaultAsync(x => x.Id == iznajmljivanje.AutomobilId);

        if (automobil == null)
        {
            return NotFound("Automobil ne postoji.");
        }

        if (automobil.TrenutnoIznajmljen == true)
        {
            return BadRequest("Automobil je vec iznajmljen.");
        }

        var korisnik = await _context.Korisnici
                        .FirstOrDefaultAsync(x => x.Id == iznajmljivanje.KorisnikId);

        if (korisnik == null)
        {
            return NotFound("Korisnik ne postoji.");
        }


        Iznajmljivanje novoIznajmljivanje = new()
        {
            Automobil = automobil,
            Korisnik = korisnik,
            BrojDana = iznajmljivanje.BrojDana
        };

        automobil.Iznajmljivanja.Add(novoIznajmljivanje);
        korisnik.Iznajmljivanja.Add(novoIznajmljivanje);

        automobil.TrenutnoIznajmljen = true;
        _context.Iznajmljivanja.Add(novoIznajmljivanje);
        int result = await _context.SaveChangesAsync();

        if (result == 0)
        {
            return BadRequest("Iznajmljivanje nije sacuvano.");
        }

        var response = new IznajmljivanjeResponse
        {
            Id = novoIznajmljivanje.Id,
            Automobil = new AutomobilBasicResponse
            {
                Id = automobil.Id,
                Model = automobil.Model,
                Kilometraza = automobil.Kilometraza,
                Godiste = automobil.Godiste,
                BrojSedista = automobil.BrojSedista,
                CenaPoDanu = automobil.CenaPoDanu,
                TrenutnoIznajmljen = automobil.TrenutnoIznajmljen
            },
            Korisnik = new KorisnikBasicResponse
            {
                Id = korisnik.Id,
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                Jmbg = korisnik.Jmbg,
                BrojVozacke = korisnik.BrojVozacke
            },
            BrojDana = novoIznajmljivanje.BrojDana
        };

        return Ok(response);
    }

    [HttpPut("iznajmljivanja")]
    public async Task<ActionResult> Update([FromBody] UpdateIznajmljivanjeRequest iznajmljivanje)
    {
        if (iznajmljivanje.Id < 1)
        {
            return BadRequest("Nevalidan Id.");
        }

        if (iznajmljivanje.AutomobilId < 1)
        {
            return BadRequest("Nevalidan AutomobilId.");
        }

        if (iznajmljivanje.KorisnikId < 1)
        {
            return BadRequest("Nevalidan KorisnikId.");
        }

        var postojeceIznajmljivanje = await _context.Iznajmljivanja
                                        .Include(x => x.Automobil)
                                        .Include(x => x.Korisnik)
                                        .FirstOrDefaultAsync(x => x.Id == iznajmljivanje.Id);
        if (postojeceIznajmljivanje == null)
        {
            return NotFound("Iznajmljivanje ne postoji.");
        }

        var automobil = await _context.Automobili.FirstOrDefaultAsync(x => x.Id == iznajmljivanje.AutomobilId);
        if (automobil == null)
        {
            return NotFound("Automobil ne postoji.");
        }

        if (automobil.TrenutnoIznajmljen == true && automobil.Id != postojeceIznajmljivanje.Automobil!.Id)
        {
            return BadRequest("Automobil je vec iznajmljen.");
        }

        var korisnik = await _context.Korisnici.FirstOrDefaultAsync(x => x.Id == iznajmljivanje.KorisnikId);
        if (korisnik == null)
        {
            return NotFound("Korisnik ne postoji.");
        }

        automobil.TrenutnoIznajmljen = true;
        postojeceIznajmljivanje.Automobil!.TrenutnoIznajmljen = false;
        postojeceIznajmljivanje.Automobil = automobil;
        postojeceIznajmljivanje.Korisnik = korisnik;
        postojeceIznajmljivanje.BrojDana = iznajmljivanje.BrojDana;

        await _context.SaveChangesAsync();

        var response = new IznajmljivanjeResponse
        {
            Id = postojeceIznajmljivanje.Id,
            Automobil = new AutomobilBasicResponse
            {
                Id = postojeceIznajmljivanje.Automobil.Id,
                Model = postojeceIznajmljivanje.Automobil.Model,
                Kilometraza = postojeceIznajmljivanje.Automobil.Kilometraza,
                Godiste = postojeceIznajmljivanje.Automobil.Godiste,
                BrojSedista = postojeceIznajmljivanje.Automobil.BrojSedista,
                CenaPoDanu = postojeceIznajmljivanje.Automobil.CenaPoDanu,
                TrenutnoIznajmljen = postojeceIznajmljivanje.Automobil.TrenutnoIznajmljen
            },
            Korisnik = new KorisnikBasicResponse
            {
                Id = postojeceIznajmljivanje.Korisnik.Id,
                Ime = postojeceIznajmljivanje.Korisnik.Ime,
                Prezime = postojeceIznajmljivanje.Korisnik.Prezime,
                Jmbg = postojeceIznajmljivanje.Korisnik.Jmbg,
                BrojVozacke = postojeceIznajmljivanje.Korisnik.BrojVozacke
            },
            BrojDana = postojeceIznajmljivanje.BrojDana
        };

        return Ok(response);
    }

    [HttpDelete("iznajmljivanja/{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        if (id < 1)
        {
            return BadRequest("Nevalidan Id.");
        }

        var iznajmljivanje = await _context.Iznajmljivanja
                                    .Include(x => x.Automobil)
                                    .FirstOrDefaultAsync(x => x.Id == id);
        if (iznajmljivanje == null)
        {
            return NotFound("Iznajmljivanje ne postoji.");
        }

        iznajmljivanje.Automobil!.TrenutnoIznajmljen = false;
        _context.Iznajmljivanja.Remove(iznajmljivanje);
        await _context.SaveChangesAsync();

        return Ok();
    }
}