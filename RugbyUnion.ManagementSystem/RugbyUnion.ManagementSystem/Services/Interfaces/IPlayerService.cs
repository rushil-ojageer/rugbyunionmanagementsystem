using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Services
{
    public interface IPlayerService
    {
        Task<PagedDto<TeamPlayerDto, TeamPlayer>> GetTeamHistory(int playerId, int offset = 0, int pageSize = 10);
    }
}
