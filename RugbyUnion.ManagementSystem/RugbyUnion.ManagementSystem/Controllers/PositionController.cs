using Microsoft.AspNetCore.Mvc;
using RugbyUnion.ManagementSystem.Controllers.Base;
using RugbyUnion.ManagementSystem.Data.Models;
using RugbyUnion.ManagementSystem.Models;
using RugbyUnion.ManagementSystem.Services;

namespace RugbyUnion.ManagementSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PositionController : BaseCrudController<PositionDto, Position>
    {
        public PositionController(ICrudService crudService) :
            base(crudService)
        {

        }
    }
}
