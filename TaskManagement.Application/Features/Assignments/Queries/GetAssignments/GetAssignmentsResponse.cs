namespace TaskManagement.Application.Features.Assignments.Queries.GetAssignments;

public sealed record GetAssignmentsResponse(List<AssignmentListItem> Assignments);