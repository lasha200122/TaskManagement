namespace TaskManagement.Api.Controllers;

[Route("")]
public class HealthController : ApiController
{
    public HealthController(ISender mediator) : base(mediator)
    {
    }

    [HttpGet]
    public IActionResult HealthCheck()
    {
        Dictionary<string, string> response = new Dictionary<string, string>
        {
            { "HealthCeck", "OK" }
        };

        return Ok(response);
    }
}

