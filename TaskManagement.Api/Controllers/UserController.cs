namespace TaskManagement.Api.Controllers;

[Route("api/user")]
[Authorize]
public class UserController : ApiController
{
    public UserController(ISender mediator) : base(mediator) {}

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand request) 
    {
        return await Handle<RegisterUserCommand, DefaultResponse>(request);
    }

    [HttpGet("users")]
    public async Task<IActionResult> Users() 
    {
        return await Handle<GetUsersQuery, GetUsersResponse>(new GetUsersQuery());
    }
}
