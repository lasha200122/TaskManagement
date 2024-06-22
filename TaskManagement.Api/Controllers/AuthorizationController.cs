namespace TaskManagement.Api.Controllers;

[Route("api/auth")]
[AllowAnonymous]
public class AuthorizationController : ApiController
{
    public AuthorizationController(ISender mediator) : base(mediator) {}

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand request) 
    {
        return await Handle<LoginCommand, AuthorizationResponse>(request);
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout() 
    {
        return await Handle<LogoutCommand, DefaultResponse>(
            new LogoutCommand(
                Guid.Parse(HttpContext.User.Claims
                .First(c => c.Type == ClaimTypes.NameIdentifier)
                .Value)));
    }
}
