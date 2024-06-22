namespace TaskManagement.Application.Features.Assignments.Queries.GetAssignments;

public class GetAssignmentsHandler : IRequestHandler<GetAssignmentsQuery, ErrorOr<GetAssignmentsResponse>>
{
    private readonly IAssignmentRepository _assignmentRepository;

    public GetAssignmentsHandler(IAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<ErrorOr<GetAssignmentsResponse>> Handle(GetAssignmentsQuery request, CancellationToken cancellationToken)
    {
        var query = _assignmentRepository.GetQueryable(x => !x.IsDeleted, x => x.User);

        if (!string.IsNullOrEmpty(request.Text))
            query = query.Where(x => x.Title.Contains(request.Text.ToLower()) || (string.IsNullOrEmpty(x.Description) || x.Description.Contains(request.Text.ToLower())));

        if (request.Status.HasValue)
            query = query.Where(x => x.Status == request.Status);

        if (request.Priority.HasValue)
            query = query.Where(x => x.Priority == request.Priority);

        var assignments = await query.Select(x => new AssignmentListItem(
            x.Id,
            x.Title,
            x.Description,
            x.Priority,
            x.Status,
            x.User == null ? string.Empty : x.User.FullName)).ToListAsync();

        return new GetAssignmentsResponse(assignments);
    }
}
