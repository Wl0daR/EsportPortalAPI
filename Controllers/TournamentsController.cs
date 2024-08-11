using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsportPortal.Models;
using EsportPortal.Data;

namespace EsportPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly EsportContext _context;

        public TournamentsController(EsportContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDto>>> GetTournaments()
        {
            var tournaments = await _context.Tournaments.ToListAsync();

            var tournamentDtos = tournaments.Select(t => new TournamentDto
            {
                Id = t.Id,
                Name = t.Name,
                Date = t.Date,
                Location = t.Location,
                LogoUrl = string.IsNullOrEmpty(t.LogoUrl) ? "/images/Tournament/NoLogo.png" : t.LogoUrl
            }).ToList();

            return tournamentDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TournamentDetailsDto>> GetTournament(int id)
        {
            var tournament = await _context.Tournaments
                .Include(t => t.TeamTournamentHistories)
                    .ThenInclude(tth => tth.Team)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (tournament == null)
            {
                return NotFound();
            }

            var tournamentDetails = new TournamentDetailsDto
            {`
                TournamentName = tournament.Name,
                LogoUrl = string.IsNullOrEmpty(t.LogoUrl) ? "/images/Tournament/NoLogo.png" : t.LogoUrl,
                Teams = tournament.TeamTournamentHistories.Select(tth => new TeamDto
                {
                    Id = tth.Team.Id,
                    Name = tth.Team.Name,
                    LogoUrl = tth.Team.LogoUrl
                }).ToList()
            };

            return tournamentDetails;
        }

        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            _context.Tournaments.Add(tournament);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTournament", new { id = tournament.Id }, tournament);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        {
            if (id != tournament.Id)
            {
                return BadRequest();
            }

            _context.Entry(tournament).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(id))
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
        public async Task<IActionResult> DeleteTournament(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TournamentExists(int id)
        {
            return _context.Tournaments.Any(e => e.Id == id);
        }
    }
}
