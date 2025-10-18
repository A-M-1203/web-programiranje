using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oktobar2025Grupa1.Models;
using Oktobar2025Grupa1.Requests;

namespace Oktobar2025Grupa1.Controllers;

[ApiController]
public class SastojakController : ControllerBase
{
    private readonly AppDbContext _context;

    public SastojakController(AppDbContext context)
    {
        _context = context;
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpGet("sastojci")]
    public async Task<ActionResult> GetAll()
    {
        return Ok(await _context.Sastojci.Select(x => x.Naziv).ToListAsync());
    }

    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost("sastojci")]
    public async Task<ActionResult> Create([FromBody] CreateSastojakRequest request)
    {
        var sastojak = await _context.Sastojci.FirstOrDefaultAsync(x => x.Naziv == request.Naziv);
        if (sastojak is not null)
        {
            return Problem
            (
                type: "Bad Request",
                title: "Sastojak vec postoji",
                detail: "Sastojak sa navedenim nazivom vec postoji",
                statusCode: StatusCodes.Status400BadRequest
            );
        }

        sastojak = new Sastojak
        {
            Naziv = request.Naziv,
            Debljina = request.Debljina,
        };

        _context.Sastojci.Add(sastojak);
        await _context.SaveChangesAsync();

        return Created();
    }
}