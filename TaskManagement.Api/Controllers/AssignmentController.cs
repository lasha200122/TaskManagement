namespace TaskManagement.Api.Controllers;

[Route("api/assignment")]
[Authorize]
public class AssignmentController : ApiController
{
    public AssignmentController(ISender mediator) : base(mediator) {}

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateAssignmentCommand request) 
    {
        return await Handle<CreateAssignmentCommand, DefaultResponse>(request);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(Guid id) 
    {
        return await Handle<DeleteAssignmentCommand, DefaultResponse>(new DeleteAssignmentCommand(id));
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateAssignmentCommand request) 
    {
        return await Handle<UpdateAssignmentCommand, DefaultResponse>(request);
    }

    [HttpGet("assignments")]
    public async Task<IActionResult> GetAssignments([FromQuery] GetAssignmentsQuery request) 
    {
        return await Handle<GetAssignmentsQuery, GetAssignmentsResponse>(request);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAssignment(Guid id) 
    {
        return await Handle<GetAssignmentQuery, AssignmentViewModel>(new GetAssignmentQuery(id));
    }
}
