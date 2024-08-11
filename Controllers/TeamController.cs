using EsportPortalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsportPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly EsportContext _context;

        public TeamsController(EsportContext context)
        {
            _context = context;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeams()
        {
            var team = await _context.Teams.Include(t => t.Players).ToListAsync();

            var teamDto = team.Select(t => new Team
            {
                Id = t.Id,
                Name = t.Name,
                LogoUrl = string.IsNullOrEmpty(t.LogoUrl) ? "/images/Team/NoLogo.png" : t.LogoUrl
            }).ToList();
            return teamDto;
        }

        // GET: api/Teams/1/Players
        [HttpGet("{id}/Players")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetTeamPlayers(int id)
        {
            var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }
            var playerDtos = team.Players.Select(p => new PlayerDto
            {
                Id = p.Id,
                Name = p.Name,
                Nickname = p.Nickname,
                Surname = p.Surname,
                TeamId = p.TeamId,
                Team = p.Team != null ? new TeamDto { Id = p.Team.Id, Name = p.Team.Name } : null,
                Role = p.Role,
                Nationality = p.Nationality,
                BirthDate = p.BirthDate, // Convert to string
                FavouriteMap = p.FavouriteMap,
                PhotoUrl = string.IsNullOrEmpty(p.PhotoUrl) ? "/images/Player/NoAvatar.png" : p.PhotoUrl
            }).ToList();
            return playerDtos;
        }

        // GET: api/Teams/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _context.Teams.Include(t => t.Players).FirstOrDefaultAsync(t => t.Id == id);

            if (team == null)
            {
                return NotFound();
            }

            return team;
        }
    }
}
