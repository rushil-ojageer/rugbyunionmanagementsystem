using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Services
{
    public interface ITeamService
    {
        Task<PlayerDto> AddPlayer(int teamId, int playerId);
        Task<PlayerDto> RemovePlayer(int teamId, int playerId);
        Task<StadiumDto> LinkStadium(int teamId, int stadiumId);
        Task<StadiumDto> UnLinkStadium(int teamId, int stadiumId);
        Task<PagedDto<TeamPlayerDto, TeamPlayer>> GetPlayerHistory(int teamId, int offset = 0, int pageSize = 10);
        Task<PagedDto<TeamStadiumDto, TeamStadium>> GetStadiumHistory(int teamId, int offset = 0, int pageSize = 10);
    }
}
