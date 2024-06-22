namespace TaskManagement.Application.Features.Assignments.Common;

public sealed record AssignmentListItem(
    Guid Id,
    string Title,
    string? Description,
    AssignmentPriority Priority,
    AssignmentStatus Status,
    string? FullName);