namespace TaskManagement.Api.Controllers;

[Route("error")]
[ApiController]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    [HttpGet]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<ExceptionHandlerFeature>()?.Error;

        return Problem();
    }
}