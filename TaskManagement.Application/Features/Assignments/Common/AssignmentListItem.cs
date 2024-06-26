﻿namespace TaskManagement.Application.Features.Assignments.Common;

public sealed record AssignmentListItem(
    Guid Id,
    string Title,
    AssignmentPriority Priority,
    AssignmentStatus Status,
    string? Letter,
    string Day);