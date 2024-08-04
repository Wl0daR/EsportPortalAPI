using EsportPortalAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace EsportPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly EsportContext _context;

        public PlayerController(EsportContext context)
        {
            _context = context;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayers()
        {
            var players = await _context.Players
                                        .Include(p => p.Team)
                                        .ToListAsync();

            var playerDtos = players.Select(p => new PlayerDto
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

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> GetPlayer(int id)
        {
            var player = await _context.Players
                                       .Include(p => p.Team)
                                       .Include(p => p.PlayerHistories)
                                       .ThenInclude(ph => ph.Team)
                                       .FirstOrDefaultAsync(p => p.Id == id);

            if (player == null)
            {
                return NotFound();
            }

            var playerDto = new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                Nickname = player.Nickname,
                Surname = player.Surname,
                TeamId = player.TeamId,
                Team = player.Team != null ? new TeamDto { Id = player.Team.Id, Name = player.Team.Name } : null,
                Role = player.Role,
                Nationality = player.Nationality,
                BirthDate = player.BirthDate, // Convert to string
                FavouriteMap = player.FavouriteMap,
                PhotoUrl = string.IsNullOrEmpty(player.PhotoUrl) ? "/images/Player/NoAvatar.png" : player.PhotoUrl,
                PlayerHistories = player.PlayerHistories?.Select(ph => new PlayerHistoryDto
                {
                    Id = ph.Id,
                    PlayerId = ph.PlayerId,
                    TeamId = ph.TeamId,
                    TeamName = ph.Team.Name,
                    StartDate = ph.StartDate,
                    EndDate = ph.EndDate
                }).ToList()
            };

            return playerDto;
        }
    }
}
