using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RugbyUnion.ManagementSystem.Models;
using System.Net;

namespace RugbyUnion.ManagementSystem.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        private readonly ILogger<ErrorsController> _logger;

        public ErrorsController(ILogger<ErrorsController> logger)
        {
            _logger = logger;
        }

        [Route("error")]
        public ErrorResponse Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error; 
            Response.StatusCode = (int) HttpStatusCode.BadRequest;
            _logger.LogError(exception?.Message ?? "An unexpected error has occurred.", exception.StackTrace);
            return new ErrorResponse(exception);
        }
    }
}
