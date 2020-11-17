using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Services
{
    public interface IStadiumService
    {
        Task<PagedDto<TeamStadiumDto, TeamStadium>> GetTeamHistory(int stadiumId, int offset = 0, int pageSize = 10);
    }
}
