using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data;
using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Services
{
    public class TeamService : ITeamService
    {
        private readonly RugbyUnionContext _context;

        public TeamService(RugbyUnionContext context)
        {
            _context = context;
        }

        public async Task<PlayerDto> AddPlayer(int teamId, int playerId)
        {
            var player = _context.Players
                .SingleOrDefault(x => x.Id == playerId);

            if (player == null)
                throw new Exception($"Unable to find player with ID {playerId}.");

            var team = _context.Teams
                .SingleOrDefault(x => x.Id == teamId);

            if (team == null)
                throw new Exception($"Unable to find team with ID {teamId}.");

            var currentTeams = _context.TeamPlayers
                .OrderByDescending(x => x.FromDate)
                .Where(x => x.PlayerId == playerId && !x.ToDate.HasValue)
                .ToList();

            if (currentTeams.Any())
            {
                foreach (var currentTeam in currentTeams)
                    currentTeam.ToDate = DateTime.Today;
            }

            if (!team.CanAddPlayer(player, out var reason))
                throw new Exception(reason);

            var teamPlayer = new TeamPlayer
            {
                PlayerId = player.Id,
                TeamId = team.Id,
                FromDate = DateTime.Today,
                PositionId = player.CurrentPositionId
            };

            _context.TeamPlayers.Add(teamPlayer);
            await _context.SaveChangesAsync();

            var dto = new PlayerDto();
            dto.Populate(player);
            return dto;
        }

        public async Task<PlayerDto> RemovePlayer(int teamId, int playerId)
        {
            var player = _context.Players
                .SingleOrDefault(x => x.Id == playerId);

            if (player == null)
                throw new Exception($"Unable to find player with ID {playerId}.");

            var team = _context.Teams
                .SingleOrDefault(x => x.Id == teamId);

            if (team == null)
                throw new Exception($"Unable to find team with ID {teamId}.");

            var currentTeams = _context.TeamPlayers
                .OrderByDescending(x => x.FromDate)
                .Where(x => x.TeamId == teamId && x.PlayerId == playerId && !x.ToDate.HasValue)
                .ToList();

            if (currentTeams.Any())
            {
                foreach (var currentTeam in currentTeams)
                    currentTeam.ToDate = DateTime.Today;
            }

            await _context.SaveChangesAsync();

            var dto = new PlayerDto();
            dto.Populate(player);
            return dto;
        }

        public async Task<StadiumDto> LinkStadium(int teamId, int stadiumId)
        {
            var stadium = _context.Stadiums
                .SingleOrDefault(x => x.Id == stadiumId);

            if (stadium == null)
                throw new Exception($"Unable to find stadium with ID {stadiumId}.");

            var team = _context.Teams
                .SingleOrDefault(x => x.Id == teamId);

            if (team == null)
                throw new Exception($"Unable to find team with ID {teamId}.");

            var currentTeams = _context.TeamStadiums
                .OrderByDescending(x => x.FromDate)
                .Where(x => x.StadiumId == stadiumId && !x.ToDate.HasValue)
                .ToList();

            if (currentTeams.Any())
            {
                foreach (var currentTeam in currentTeams)
                    currentTeam.ToDate = DateTime.Today;
            }

            if (!team.CanPlayInStadium(stadium, out var reason))
                throw new Exception(reason);

            var teamStadium = new TeamStadium
            {
                StadiumId = stadium.Id,
                TeamId = team.Id,
                FromDate = DateTime.Today
            };

            _context.TeamStadiums.Add(teamStadium);
            await _context.SaveChangesAsync();

            var dto = new StadiumDto();
            dto.Populate(stadium);
            return dto;
        }

        public async Task<StadiumDto> UnLinkStadium(int teamId, int stadiumId)
        {
            var stadium = _context.Stadiums
                .SingleOrDefault(x => x.Id == stadiumId);

            if (stadium == null)
                throw new Exception($"Unable to find stadium with ID {stadiumId}.");

            var team = _context.Teams
                .SingleOrDefault(x => x.Id == teamId);

            if (team == null)
                throw new Exception($"Unable to find team with ID {teamId}.");

            var currentTeams = _context.TeamStadiums
                .OrderByDescending(x => x.FromDate)
                .Where(x => x.TeamId == teamId && x.StadiumId == stadiumId && !x.ToDate.HasValue)
                .ToList();

            if (currentTeams.Any())
            {
                foreach (var currentTeam in currentTeams)
                    currentTeam.ToDate = DateTime.Today;
            }

            await _context.SaveChangesAsync();

            var dto = new StadiumDto();
            dto.Populate(stadium);
            return dto;
        }

        public async Task<PagedDto<TeamPlayerDto, TeamPlayer>> GetPlayerHistory(int teamId, int offset = 0, int pageSize = 10)
        {
            var query = _context.TeamPlayers
                .Where(x => x.TeamId == teamId);

            var totalItems = query.Count();

            var items = await query.OrderBy(x => x.FromDate)
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync();

            var dtos = items
                .Select(x =>
                {
                    var dto = new TeamPlayerDto();
                    dto.Populate(x);
                    return dto;
                })
                .ToList();

            var pagedDto = new PagedDto<TeamPlayerDto, TeamPlayer>
            {
                Items = dtos,
                TotalItems = totalItems,
                Offset = offset,
                Count = pageSize
            };

            return pagedDto;
        }

        public async Task<PagedDto<TeamStadiumDto, TeamStadium>> GetStadiumHistory(int teamId, int offset = 0, int pageSize = 10)
        {
            var query = _context.TeamStadiums
                .Where(x => x.TeamId == teamId);

            var totalItems = query.Count();

            var items = await query.OrderBy(x => x.FromDate)
                .Skip(offset)
                .Take(pageSize)
                .ToListAsync();

            var dtos = items
                .Select(x =>
                {
                    var dto = new TeamStadiumDto();
                    dto.Populate(x);
                    return dto;
                })
                .ToList();

            var pagedDto = new PagedDto<TeamStadiumDto, TeamStadium>
            {
                Items = dtos,
                TotalItems = totalItems,
                Offset = offset,
                Count = pageSize
            };

            return pagedDto;
        }
    }
}
