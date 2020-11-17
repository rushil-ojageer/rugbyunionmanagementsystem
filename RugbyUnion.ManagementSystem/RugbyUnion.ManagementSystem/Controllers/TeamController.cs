using Microsoft.AspNetCore.Mvc;
using RugbyUnion.ManagementSystem.Controllers.Base;
using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models;
using RugbyUnion.ManagementSystem.Services;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : BaseCrudController<TeamDto, Team>
    {
        private readonly ITeamService _teamService;

        public TeamController(ICrudService crudService, ITeamService teamService) :
            base(crudService)
        {
            _teamService = teamService;
        }

        [HttpPost("{teamId}/add/player/{playerId}")]
        public async Task<PlayerDto> AddPlayer(int teamId, int playerId)
        {
            return await _teamService.AddPlayer(teamId, playerId);
        }

        [HttpDelete("{teamId}/remove/player/{playerId}")]
        public async Task<PlayerDto> RemovePlayer(int teamId, int playerId)
        {
            return await _teamService.RemovePlayer(teamId, playerId);
        }

        [HttpPost("{teamId}/link/stadium/{stadiumId}")]
        public async Task<StadiumDto> LinkStadium(int teamId, int stadiumId)
        {
            return await _teamService.LinkStadium(teamId, stadiumId);
        }

        [HttpDelete("{teamId}/unlink/stadium/{stadiumId}")]
        public async Task<StadiumDto> UnLinkStadium(int teamId, int stadiumId)
        {
            return await _teamService.UnLinkStadium(teamId, stadiumId);
        }

        [HttpGet("{teamId}/player/history")]
        public async Task<PagedDto<TeamPlayerDto, TeamPlayer>> GetPlayerHistory(int teamId, int offset = 0, int pageSize = 10)
        {
            return await _teamService.GetPlayerHistory(teamId, offset, pageSize);
        }

        [HttpGet("{teamId}/stadium/history")]
        public async Task<PagedDto<TeamStadiumDto, TeamStadium>> GetStadiumHistory(int teamId, int offset = 0, int pageSize = 10)
        {
            return await _teamService.GetStadiumHistory(teamId, offset, pageSize);
        }
    }
}
