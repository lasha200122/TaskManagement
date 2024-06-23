namespace TaskManagement.Application.Features.Assignments.Queries.GetAssignments;

public sealed record GetAssignmentsQuery(
    string? Text,
    AssignmentPriority? Priority,
    Guid? UserId) : IRequest<ErrorOr<GetAssignmentsResponse>>;