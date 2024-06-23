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

        if (request.UserId.HasValue)
            query = query.Where(x => x.UserId == request.UserId);

        if (request.Priority.HasValue)
            query = query.Where(x => x.Priority == request.Priority);

        var assignments = await query.Select(x => new AssignmentListItem(
            x.Id,
            x.Title,
            x.Priority,
            x.Status,
            x.User == null ? string.Empty : x.User.FullName[0].ToString().ToUpper(),
            (x.DueDate - DateTime.Now).Days == 0 ? "0 day" :
            (x.DueDate - DateTime.Now).Days == 1 ? "1 day" :
            (x.DueDate - DateTime.Now).Days + " days")).ToListAsync();

        return new GetAssignmentsResponse(assignments);
    }
}
