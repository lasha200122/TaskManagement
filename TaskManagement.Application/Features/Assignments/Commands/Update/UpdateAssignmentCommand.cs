namespace TaskManagement.Application.Features.Assignments.Commands.Update;

public sealed record UpdateAssignmentCommand(
    Guid Id,
    string Title,
    string? Description,
    DateTimeOffset DueDate,
    AssignmentPriority Priority,
    AssignmentStatus Status,
    Guid? UserId) : IRequest<ErrorOr<DefaultResponse>>;