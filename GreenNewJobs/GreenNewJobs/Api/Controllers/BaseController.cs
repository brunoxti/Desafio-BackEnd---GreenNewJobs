using GreenNewJobs.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace GreenNewJobs.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result, CancellationToken cancellationToken)
        {
            if (!result.IsSuccess)
            {
                return BadRequest(new { Errors = result.Errors });
            }
            return Ok(result.Value);
        }
    }
}
