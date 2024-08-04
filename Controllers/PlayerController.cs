﻿// Controllers/PlayersController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PlayersController : ControllerBase
{
    private readonly EsportContext _context;

    public PlayersController(EsportContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
    {
        return await _context.Players.Include(p => p.Team).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Player>> GetPlayer(int id)
    {
        var player = await _context.Players.Include(p => p.Team)
                                           .FirstOrDefaultAsync(p => p.Id == id);

        if (player == null)
        {
            return NotFound();
        }

        return player;
    }

    [HttpPost]
    public async Task<ActionResult<Player>> PostPlayer(Player player)
    {
        _context.Players.Add(player);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPlayer", new { id = player.Id }, player);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutPlayer(int id, Player player)
    {
        if (id != player.Id)
        {
            return BadRequest();
        }

        _context.Entry(player).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PlayerExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlayer(int id)
    {
        var player = await _context.Players.FindAsync(id);
        if (player == null)
        {
            return NotFound();
        }

        _context.Players.Remove(player);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PlayerExists(int id)
    {
        return _context.Players.Any(e => e.Id == id);
    }
}
