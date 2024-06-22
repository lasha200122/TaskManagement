namespace TaskManagement.Application.Features.Assignments.Commands.Delete;

public sealed record DeleteAssignmentCommand(Guid Id) : IRequest<ErrorOr<DefaultResponse>>;