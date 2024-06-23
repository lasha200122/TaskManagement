namespace TaskManagement.Application.Features.Assignments.Queries.GetAssignment;

public sealed record GetAssignmentQuery(Guid Id) : IRequest<ErrorOr<AssignmentViewModel>>;