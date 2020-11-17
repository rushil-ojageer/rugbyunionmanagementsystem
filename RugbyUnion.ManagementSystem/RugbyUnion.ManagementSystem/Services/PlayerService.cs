using Microsoft.EntityFrameworkCore;
using RugbyUnion.ManagementSystem.Data;
using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyUnion.ManagementSystem.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly RugbyUnionContext _context;

        public PlayerService(RugbyUnionContext context)
        {
            _context = context;
        }

        public async Task<PagedDto<TeamPlayerDto, TeamPlayer>> GetTeamHistory(int playerId, int offset = 0, int pageSize = 10)
        {
            var query = _context.TeamPlayers
                .Where(x => x.PlayerId == playerId);

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
    }
}
