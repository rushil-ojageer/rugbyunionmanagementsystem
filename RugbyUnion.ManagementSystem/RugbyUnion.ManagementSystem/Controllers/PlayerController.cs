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
    public class PlayerController : BaseCrudController<PlayerDto, Player>
    {
        private readonly IPlayerService _playerService;
        private readonly ITeamService _teamService;

        public PlayerController(ICrudService crudService, IPlayerService playerService, ITeamService teamService) :
            base (crudService)
        {
            _playerService = playerService;
            _teamService = teamService;
        }

        [HttpPost("{playerId}/transfer/team/{teamId}")]
        public async Task<PlayerDto> TransferPlayer(int playerId, int teamId)
        {
            return await _teamService.AddPlayer(teamId, playerId);
        }

        [HttpGet("{playerId}/team/history")]
        public async Task<PagedDto<TeamPlayerDto, TeamPlayer>> GetPlayerHistory(int playerId, int offset = 0, int pageSize = 10)
        {
            return await _playerService.GetTeamHistory(playerId, offset, pageSize);
        }
    }
}
