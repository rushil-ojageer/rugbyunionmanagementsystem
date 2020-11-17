using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data;
using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Services
{
    public class StadiumService : IStadiumService
    {
        private RugbyUnionContext _context;

        public StadiumService(RugbyUnionContext context)
        {
            _context = context;
        }

        public async Task<PagedDto<TeamStadiumDto, TeamStadium>> GetTeamHistory(int stadiumId, int offset = 0, int pageSize = 10)
        {
            var query = _context.TeamStadiums
                .Where(x => x.StadiumId == stadiumId);

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
