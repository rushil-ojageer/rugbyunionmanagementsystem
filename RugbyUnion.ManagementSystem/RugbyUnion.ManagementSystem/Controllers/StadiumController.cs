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
    public class StadiumController : BaseCrudController<StadiumDto, Stadium>
    {
        private readonly IStadiumService _stadiumService;

        public StadiumController(ICrudService crudService, IStadiumService stadiumService) : 
            base(crudService)
        {
            _stadiumService = stadiumService;
        }

        [HttpGet("{stadiumId}/team/history")]
        public async Task<PagedDto<TeamStadiumDto, TeamStadium>> GetStadiumHistory(int stadiumId, int offset = 0, int pageSize = 10)
        {
            return await _stadiumService.GetTeamHistory(stadiumId, offset, pageSize);
        }

    }
}
