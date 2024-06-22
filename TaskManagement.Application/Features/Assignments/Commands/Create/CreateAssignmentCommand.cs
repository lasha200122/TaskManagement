namespace TaskManagement.Application.Features.Assignments.Commands.Create;

public sealed record CreateAssignmentCommand(
    string Title,
    string? Description,
    DateTimeOffset DueDate,
    AssignmentPriority Priority,
    AssignmentStatus Status,
    Guid? UserId) : IRequest<ErrorOr<DefaultResponse>>;
