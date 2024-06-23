
namespace TaskManagement.Application.Features.Assignments.Queries.GetAssignment;

public class GetAssignmentHandler : IRequestHandler<GetAssignmentQuery, ErrorOr<AssignmentViewModel>>
{
    private readonly IAssignmentRepository _assignmentRepository;

    public GetAssignmentHandler(IAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<ErrorOr<AssignmentViewModel>> Handle(GetAssignmentQuery request, CancellationToken cancellationToken)
    {
        if (await _assignmentRepository.GetSingleOrDefaultAsync(x => x.Id == request.Id) is not Assignment assignment)
            return Error.NotFound(description:"Can't find assignment");

        if (assignment.IsDeleted) return Error.Conflict(description:"Assignment deleted");

        var result = new AssignmentViewModel(
            assignment.Id,
            assignment.Title,
            assignment.Description,
            assignment.DueDate,
            assignment.Priority,
            assignment.Status,
            assignment.UserId);

        return result;
    }
}
