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
        public TeamController(ICrudService crudService) :
            base(crudService)
        {
        }
    }
}
