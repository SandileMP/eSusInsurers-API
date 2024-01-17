using eSusInsurers.Filter;
using Microsoft.AspNetCore.Mvc;

namespace eSusInsurers.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public class BaseController : ControllerBase
    {
    }
}
