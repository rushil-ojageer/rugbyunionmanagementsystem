using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RugbyUnion.ManagementSystem.Models;
using System.Net;

namespace RugbyUnion.ManagementSystem.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; 
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            return new ErrorResponse(exception);
        }
    }
}
