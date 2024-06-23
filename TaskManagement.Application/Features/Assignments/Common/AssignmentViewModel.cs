namespace TaskManagement.Application.Features.Assignments.Common;

public sealed record AssignmentViewModel(
    Guid Id,
    string Title,
    string? Description,
    DateTimeOffset DueDate,
    AssignmentPriority Priority,
    AssignmentStatus Status,
    Guid? UserId);